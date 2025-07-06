using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARC.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddName_arToRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // Update Name_ar for specific roles
            migrationBuilder.Sql("UPDATE Roles SET Name_ar = N'مسؤول' WHERE Id = 1");
            migrationBuilder.Sql("UPDATE Roles SET Name_ar = N'موظف الاستقبال' WHERE Id = 2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name_ar",
                table: "Roles");
        }
    }
}
