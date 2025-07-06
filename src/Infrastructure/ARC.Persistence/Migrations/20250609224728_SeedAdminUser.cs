using ARC.Persistence.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARC.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (SqlMigrationHelper.IsTesting())
                return;

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BirthDate", "CallPhoneNumber", "FirstName", "Gender", "LastName", "NationalIdNumber", "NationalityId", "PassportNumber", "PersonalImageURL", "SecondName", "ThirdName" },
                values: new object[] { 1, new DateTime(2001, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "01148425889", "Ahmed", false, "Khalil", "30225485672598", 64, null, null, "Magdy", "Mostafa" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "aa5a51aa-162d-4e9e-805c-c76645da10f2", "ahmed.magdy.dev9@gmail.com", true, false, null, "AHMED.MAGDY.DEV9@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEBCAwMGhwLxOSyC+U8pfBQy8SawEMXvexJEF5+QVFM5WCinzdOj2y1mcwO6FgaF3HA==", 1, "01148425889", true, "aa5a51aa-162d-4e9e-805c-c76645da10f2", false, "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
