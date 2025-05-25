using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARC.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsoAlpha2 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Placements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Placements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResearchTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentResearchTypeId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchTypes_ResearchTypes_ParentResearchTypeId",
                        column: x => x.ParentResearchTypeId,
                        principalTable: "ResearchTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThirdName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CallPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalIdNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NationalityId = table.Column<int>(type: "int", nullable: false),
                    PersonalImageURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Countries_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResearchQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText_ar = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    QuestionText_en = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ResearchTypeId = table.Column<int>(type: "int", nullable: false),
                    ParentQuestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchQuestions_ResearchQuestions_ParentQuestionId",
                        column: x => x.ParentQuestionId,
                        principalTable: "ResearchQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResearchQuestions_ResearchTypes_ResearchTypeId",
                        column: x => x.ResearchTypeId,
                        principalTable: "ResearchTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChildParents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionAssessmentId = table.Column<int>(type: "int", nullable: false),
                    ParentPersonId = table.Column<int>(type: "int", nullable: false),
                    ParentType = table.Column<bool>(type: "bit", nullable: false),
                    EmploymentStatusId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildParents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildParents_EmploymentStatuses_EmploymentStatusId",
                        column: x => x.EmploymentStatusId,
                        principalTable: "EmploymentStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChildParents_People_ParentPersonId",
                        column: x => x.ParentPersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Centers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Centers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Centers_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResidenceInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    HasFixedResidence = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidenceInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResidenceInfos_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdmissionAssessments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    InterviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EducationalLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmploymentStatusId = table.Column<int>(type: "int", nullable: false),
                    ResidenceInfoId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdmissionAssessments_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdmissionAssessments_EmploymentStatuses_EmploymentStatusId",
                        column: x => x.EmploymentStatusId,
                        principalTable: "EmploymentStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdmissionAssessments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdmissionAssessments_ResidenceInfos_ResidenceInfoId",
                        column: x => x.ResidenceInfoId,
                        principalTable: "ResidenceInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientPlacements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionAssessmentId = table.Column<int>(type: "int", nullable: false),
                    PlacementId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPlacements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientPlacements_AdmissionAssessments_AdmissionAssessmentId",
                        column: x => x.AdmissionAssessmentId,
                        principalTable: "AdmissionAssessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientPlacements_Placements_PlacementId",
                        column: x => x.PlacementId,
                        principalTable: "Placements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Researches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionAssessmentId = table.Column<int>(type: "int", nullable: false),
                    ResearchTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Researches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Researches_AdmissionAssessments_AdmissionAssessmentId",
                        column: x => x.AdmissionAssessmentId,
                        principalTable: "AdmissionAssessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Researches_ResearchTypes_ResearchTypeId",
                        column: x => x.ResearchTypeId,
                        principalTable: "ResearchTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContactAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EmergencyContactsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContactAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyContactAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionAssessmentId = table.Column<int>(type: "int", nullable: false),
                    EmergentPersonId = table.Column<int>(type: "int", nullable: false),
                    ContactAddressId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyContacts_EmergencyContactAddresses_ContactAddressId",
                        column: x => x.ContactAddressId,
                        principalTable: "EmergencyContactAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmergencyContacts_People_EmergentPersonId",
                        column: x => x.EmergentPersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResearchQuestionId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnswerType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    ParentChoiceId = table.Column<int>(type: "int", nullable: true),
                    ParentBooleanValue = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionVersions_ResearchQuestions_ResearchQuestionId",
                        column: x => x.ResearchQuestionId,
                        principalTable: "ResearchQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResearchQuestionChoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionVersionId = table.Column<int>(type: "int", nullable: false),
                    Choice_ar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Choice_en = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchQuestionChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchQuestionChoices_QuestionVersions_QuestionVersionId",
                        column: x => x.QuestionVersionId,
                        principalTable: "QuestionVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearchQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResearchId = table.Column<int>(type: "int", nullable: false),
                    QuestionVersionId = table.Column<int>(type: "int", nullable: false),
                    IsDependant = table.Column<bool>(type: "bit", nullable: false),
                    ChoiceId = table.Column<int>(type: "int", nullable: true),
                    NumericAnswer = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FreeText = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    BooleanAnswer = table.Column<bool>(type: "bit", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchQuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchQuestionAnswers_QuestionVersions_QuestionVersionId",
                        column: x => x.QuestionVersionId,
                        principalTable: "QuestionVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResearchQuestionAnswers_ResearchQuestionChoices_ChoiceId",
                        column: x => x.ChoiceId,
                        principalTable: "ResearchQuestionChoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResearchQuestionAnswers_Researches_ResearchId",
                        column: x => x.ResearchId,
                        principalTable: "Researches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResearchAnswerChoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResearchQuestionAnswerId = table.Column<int>(type: "int", nullable: false),
                    ResearchQuestionChoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchAnswerChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchAnswerChoices_ResearchQuestionAnswers_ResearchQuestionAnswerId",
                        column: x => x.ResearchQuestionAnswerId,
                        principalTable: "ResearchQuestionAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResearchAnswerChoices_ResearchQuestionChoices_ResearchQuestionChoiceId",
                        column: x => x.ResearchQuestionChoiceId,
                        principalTable: "ResearchQuestionChoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionAssessments_CenterId",
                table: "AdmissionAssessments",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionAssessments_EmploymentStatusId",
                table: "AdmissionAssessments",
                column: "EmploymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionAssessments_PatientId",
                table: "AdmissionAssessments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionAssessments_ResidenceInfoId",
                table: "AdmissionAssessments",
                column: "ResidenceInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Centers_CityId",
                table: "Centers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Centers_StateId",
                table: "Centers",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildParents_EmploymentStatusId",
                table: "ChildParents",
                column: "EmploymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildParents_ParentPersonId",
                table: "ChildParents",
                column: "ParentPersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContactAddresses_AddressId",
                table: "EmergencyContactAddresses",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContactAddresses_EmergencyContactsId",
                table: "EmergencyContactAddresses",
                column: "EmergencyContactsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContacts_ContactAddressId",
                table: "EmergencyContacts",
                column: "ContactAddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContacts_EmergentPersonId",
                table: "EmergencyContacts",
                column: "EmergentPersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientPlacements_AdmissionAssessmentId",
                table: "PatientPlacements",
                column: "AdmissionAssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPlacements_PlacementId",
                table: "PatientPlacements",
                column: "PlacementId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PersonId",
                table: "Patients",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_NationalityId",
                table: "People",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionVersions_ParentChoiceId",
                table: "QuestionVersions",
                column: "ParentChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionVersions_ResearchQuestionId",
                table: "QuestionVersions",
                column: "ResearchQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchAnswerChoices_ResearchQuestionAnswerId",
                table: "ResearchAnswerChoices",
                column: "ResearchQuestionAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchAnswerChoices_ResearchQuestionChoiceId",
                table: "ResearchAnswerChoices",
                column: "ResearchQuestionChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Researches_AdmissionAssessmentId",
                table: "Researches",
                column: "AdmissionAssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Researches_ResearchTypeId",
                table: "Researches",
                column: "ResearchTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchQuestionAnswers_ChoiceId",
                table: "ResearchQuestionAnswers",
                column: "ChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchQuestionAnswers_QuestionVersionId",
                table: "ResearchQuestionAnswers",
                column: "QuestionVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchQuestionAnswers_ResearchId",
                table: "ResearchQuestionAnswers",
                column: "ResearchId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchQuestionChoices_QuestionVersionId",
                table: "ResearchQuestionChoices",
                column: "QuestionVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchQuestions_ParentQuestionId",
                table: "ResearchQuestions",
                column: "ParentQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchQuestions_ResearchTypeId",
                table: "ResearchQuestions",
                column: "ResearchTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchTypes_ParentResearchTypeId",
                table: "ResearchTypes",
                column: "ParentResearchTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidenceInfos_AddressId",
                table: "ResidenceInfos",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonId",
                table: "Users",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyContactAddresses_EmergencyContacts_EmergencyContactsId",
                table: "EmergencyContactAddresses",
                column: "EmergencyContactsId",
                principalTable: "EmergencyContacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionVersions_ResearchQuestionChoices_ParentChoiceId",
                table: "QuestionVersions",
                column: "ParentChoiceId",
                principalTable: "ResearchQuestionChoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Cities_CityId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyContacts_People_EmergentPersonId",
                table: "EmergencyContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyContactAddresses_Addresses_AddressId",
                table: "EmergencyContactAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyContactAddresses_EmergencyContacts_EmergencyContactsId",
                table: "EmergencyContactAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionVersions_ResearchQuestionChoices_ParentChoiceId",
                table: "QuestionVersions");

            migrationBuilder.DropTable(
                name: "ChildParents");

            migrationBuilder.DropTable(
                name: "PatientPlacements");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "ResearchAnswerChoices");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Placements");

            migrationBuilder.DropTable(
                name: "ResearchQuestionAnswers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Researches");

            migrationBuilder.DropTable(
                name: "AdmissionAssessments");

            migrationBuilder.DropTable(
                name: "Centers");

            migrationBuilder.DropTable(
                name: "EmploymentStatuses");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "ResidenceInfos");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "EmergencyContacts");

            migrationBuilder.DropTable(
                name: "EmergencyContactAddresses");

            migrationBuilder.DropTable(
                name: "ResearchQuestionChoices");

            migrationBuilder.DropTable(
                name: "QuestionVersions");

            migrationBuilder.DropTable(
                name: "ResearchQuestions");

            migrationBuilder.DropTable(
                name: "ResearchTypes");
        }
    }
}
