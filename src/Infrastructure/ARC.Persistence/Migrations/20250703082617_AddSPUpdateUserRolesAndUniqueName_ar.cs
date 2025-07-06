using ARC.Persistence.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARC.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSPUpdateUserRolesAndUniqueName_ar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name_ar",
                table: "Roles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name_ar",
                table: "Roles",
                column: "Name_ar",
                unique: true);

            var sql = SqlMigrationHelper.LoadSql("UpdateUserRolesStoredProcedure.sql");
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Roles_Name_ar",
                table: "Roles");

            migrationBuilder.AlterColumn<string>(
                name: "Name_ar",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.Sql("DROP PROCEDURE [dbo].[UpdateUserRoles]");
        }
    }
}
