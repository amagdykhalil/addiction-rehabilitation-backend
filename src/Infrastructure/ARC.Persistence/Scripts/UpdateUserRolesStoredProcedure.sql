
GO

/****** Object:  UserDefinedTableType [dbo].[IdList]    Script Date: 7/3/2025 10:05:48 AM ******/
CREATE TYPE [dbo].[IdList] AS TABLE(
	[Id] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO



GO

CREATE PROCEDURE dbo.UpdateUserRoles
(
    @UserId INT,
    @Ids  dbo.IdList READONLY
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        ------------------------------------------------------------
        -- 1) If no IDs passed (or empty TVP), clear out all roles
        ------------------------------------------------------------
        IF  NOT EXISTS (SELECT 1 FROM @Ids)
        BEGIN
            DELETE UR
            FROM dbo.UserRoles AS UR
            WHERE UR.UserId = @UserId;

            COMMIT TRANSACTION;
            RETURN;
        END

        ------------------------------------------------------------
        -- 2) Delete roles that are no longer in the incoming list
        ------------------------------------------------------------
        DELETE UR
        FROM dbo.UserRoles AS UR
        LEFT JOIN @Ids AS I
          ON UR.RoleId = I.Id
        WHERE UR.UserId = @UserId
          AND I.Id IS NULL;

        ------------------------------------------------------------
        -- 3) Insert new roles that the user doesn't already have
        ------------------------------------------------------------
        INSERT INTO dbo.UserRoles (UserId, RoleId)
        SELECT DISTINCT
            @UserId,
            I.Id
        FROM @Ids AS I
        WHERE NOT EXISTS
        (
            SELECT 1
            FROM dbo.UserRoles AS UR2
            WHERE UR2.UserId = @UserId
              AND UR2.RoleId = I.Id
        );

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0
            ROLLBACK TRANSACTION;

        -- Re‑throw the error for the caller to handle/log
        THROW;
    END CATCH
END
GO
