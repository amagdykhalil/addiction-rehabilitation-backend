using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ARC.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsoAlpha2",
                table: "Countries",
                newName: "Iso3");

            migrationBuilder.RenameColumn(
                name: "CountryCode",
                table: "Countries",
                newName: "Code");

            migrationBuilder.AlterColumn<byte>(
                name: "MaritalStatus",
                table: "AdmissionAssessments",
                type: "tinyint",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<byte>(
                name: "EducationalLevel",
                table: "AdmissionAssessments",
                type: "tinyint",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "EmploymentStatuses",
                columns: new[] { "Id", "Name_ar", "Name_en" },
                values: new object[,]
                {
                    { 1, "عاطل عن العمل", "Unemployed" },
                    { 2, "عمل مستقل", "Self Employed" },
                    { 3, "يعمل ويدرس", "Working & Studying" },
                    { 4, "لا يعمل ويدرس", "Not Working & Studying" },
                    { 5, "ربة منزل", "Homemaker" },
                    { 6, "متقاعد", "Retired" },
                    { 7, "لا يوجد رد", "No Response" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Admin", "ADMIN" },
                    { 2, null, "Receptionist", "RECEPTIONIST" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmploymentStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EmploymentStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmploymentStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EmploymentStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EmploymentStatuses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EmploymentStatuses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EmploymentStatuses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "Iso3",
                table: "Countries",
                newName: "IsoAlpha2");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Countries",
                newName: "CountryCode");

            migrationBuilder.AlterColumn<string>(
                name: "MaritalStatus",
                table: "AdmissionAssessments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "EducationalLevel",
                table: "AdmissionAssessments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldMaxLength: 50);
        }
    }
}
