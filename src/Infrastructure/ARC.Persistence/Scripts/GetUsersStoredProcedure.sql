
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[GetUsers]
    @SearchQuery   NVARCHAR(500) = NULL,
    @Gender        INT            = NULL,
    @NationalityId INT            = NULL,
    @RoleId        INT            = NULL,
    @IsActive      BIT            = NULL,
    @SortBy        NVARCHAR(50)   = 'Id',
    @SortDirection NVARCHAR(10)   = 'ASC',
    @PageNumber    INT            = 1,
    @PageSize      INT            = 10,
	@locale		   NVARCHAR(10)   = 'en'
AS
BEGIN
    SET NOCOUNT ON;

    --------------------------------------------------------------------------------
    -- 1) Prepare any derived values
    --------------------------------------------------------------------------------
    DECLARE 
        @Offset          INT = (@PageNumber - 1) * @PageSize,
        @SearchPattern   NVARCHAR(502),
        @CountWhereClause NVARCHAR(MAX),
        @SQL             NVARCHAR(MAX),
        @ParamList       NVARCHAR(MAX);

    IF @SearchQuery IS NOT NULL AND LEN(TRIM(@SearchQuery)) > 0
        SET @SearchPattern = N'%' + @SearchQuery + N'%';

    --------------------------------------------------------------------------------
    -- 2) Build a single WHERE‐clause fragment, using parameter placeholders
    --------------------------------------------------------------------------------
    DECLARE @WhereClause NVARCHAR(MAX) = N'';
    IF @SearchPattern IS NOT NULL
    BEGIN
        SET @WhereClause += N'
        AND (
             (p.FirstName + '' '' + p.SecondName + '' '' 
              + ISNULL(p.ThirdName,'''') + '' '' + p.LastName) 
             LIKE @SearchPattern
          OR p.CallPhoneNumber LIKE @SearchPattern
          OR p.NationalIdNumber  LIKE @SearchPattern
          OR p.PassportNumber    LIKE @SearchPattern
          OR u.Email             LIKE @SearchPattern
        )';
    END

    IF @Gender IS NOT NULL
        SET @WhereClause += N' AND p.Gender = @Gender';

    IF @NationalityId IS NOT NULL
        SET @WhereClause += N' AND p.NationalityId = @NationalityId';

    IF @IsActive IS NOT NULL
    BEGIN
        IF @IsActive = 1
            SET @WhereClause += N' AND u.DeletedAt IS NULL';
        ELSE
            SET @WhereClause += N' AND u.DeletedAt IS NOT NULL';
    END

    IF @RoleId IS NOT NULL AND @RoleId > 0
        SET @WhereClause += N'
        AND u.Id IN (
            SELECT ur.UserId
              FROM dbo.UserRoles ur
              JOIN dbo.Roles      r  ON ur.RoleId = r.Id
             WHERE r.Id = @RoleId
        )';

    -- prepare a copy of that WHERE clause for the COUNT(*) subquery,
    -- rebinding aliases p→p2, u→u2
    SET @CountWhereClause = REPLACE(
                              REPLACE(@WhereClause, 'p.', 'p2.'), 
                              'u.', 'u2.'
                           );

    --------------------------------------------------------------------------------
    -- 3) Build ORDER BY clauses (still dynamic for column names)
    --------------------------------------------------------------------------------
    DECLARE 
        @RowNumberOrderBy NVARCHAR(200),   -- for ROW_NUMBER() inside CTE
        @FinalOrderBy     NVARCHAR(200);   -- for outer SELECT

    -- choose which column to sort on
    IF @SortBy = 'FirstName'
    BEGIN
        SET @RowNumberOrderBy = N'ORDER BY p.FirstName';
        SET @FinalOrderBy     = N'ORDER BY ud.FirstName';
    END
    ELSE IF @SortBy = 'LastName'
    BEGIN
        SET @RowNumberOrderBy = N'ORDER BY p.LastName';
        SET @FinalOrderBy     = N'ORDER BY ud.LastName';
    END
    ELSE IF @SortBy = 'NationalId'
    BEGIN
        SET @RowNumberOrderBy = N'ORDER BY ISNULL(p.NationalIdNumber, p.PassportNumber)';
        SET @FinalOrderBy     = N'ORDER BY ud.NationalIdNumber';  
        -- outer alias must match one of the selected columns
    END
    ELSE
    BEGIN
        SET @RowNumberOrderBy = N'ORDER BY u.Id';
        SET @FinalOrderBy     = N'ORDER BY ud.Id';
    END

    -- append direction
    IF UPPER(@SortDirection) = 'DESC'
    BEGIN
        SET @RowNumberOrderBy += N' DESC';
        SET @FinalOrderBy     += N' DESC';
    END
    ELSE
    BEGIN
        SET @RowNumberOrderBy += N' ASC';
        SET @FinalOrderBy     += N' ASC';
    END

    --------------------------------------------------------------------------------
    -- 4) Assemble the dynamic SQL
    --------------------------------------------------------------------------------
    SET @SQL = N'
    WITH UserData AS (
        SELECT 
            u.Id,
            u.Email,
            u.PersonId,
            u.DeletedAt,
            u.DeletedBy,
            p.FirstName,
            p.SecondName,
            p.ThirdName,
            p.LastName,
            p.Gender,
            p.CallPhoneNumber,
            p.NationalIdNumber,
            p.PassportNumber,
            c.Id as NationalityId,
			case when @locale = ''en'' then c.Name_en else c.Name_ar end as NationalityName,
            p.PersonalImageURL,
            ROW_NUMBER() OVER (' + @RowNumberOrderBy + N') AS RowNum
        FROM dbo.Users u
        JOIN dbo.People p 
          ON u.PersonId = p.Id
		JOIN dbo.Countries c
		  ON p.NationalityId = c.Id
        WHERE 1=1' + @WhereClause + N'
    ),
    RoleAgg AS (
        SELECT 
            ud.Id,
             STRING_AGG(
			CASE
				WHEN @locale = ''en'' THEN r.Name
				ELSE r.Name_ar
			END,
			'',''
			) AS Roles
        FROM UserData ud
        LEFT JOIN dbo.UserRoles ur 
          ON ud.Id = ur.UserId
        LEFT JOIN dbo.Roles r 
          ON ur.RoleId = r.Id
        GROUP BY ud.Id
    )
    SELECT
        ud.*,
        ra.Roles,
        (   SELECT COUNT(*) 
            FROM dbo.Users u2
            JOIN dbo.People p2 
              ON u2.PersonId = p2.Id
            WHERE 1=1' + @CountWhereClause + N'
        ) AS TotalCount
    FROM UserData ud
    LEFT JOIN RoleAgg ra 
      ON ud.Id = ra.Id
    WHERE ud.RowNum BETWEEN @Offset + 1 AND @Offset + @PageSize
    ' + @FinalOrderBy + N';';

    --------------------------------------------------------------------------------
    -- 5) Declare the parameter list and execute
    --------------------------------------------------------------------------------
    SET @ParamList = N'
        @SearchPattern   NVARCHAR(502),
        @Gender          INT,
        @NationalityId   INT,
        @RoleId          INT,
        @Offset          INT,
        @PageSize        INT,
		@locale		     NVARCHAR(10)
    ';

    EXEC sp_executesql
        @SQL,
        @ParamList,
        @SearchPattern  = @SearchPattern,
        @Gender         = @Gender,
        @NationalityId  = @NationalityId,
        @RoleId         = @RoleId,
        @Offset         = @Offset,
        @PageSize       = @PageSize,
		@locale			= @locale;
END;
