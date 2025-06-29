using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARC.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "People",
                type: "bit",
                nullable: false,
                comment: "stores 1 for Female, 0 for Male",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BirthDate",
                table: "Patients",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_People_NationalIdNumber",
                table: "People",
                column: "NationalIdNumber",
                unique: true,
                filter: "[NationalIdNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_People_PassportNumber",
                table: "People",
                column: "PassportNumber",
                unique: true,
                filter: "[PassportNumber] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_NationalIdNumber",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_PassportNumber",
                table: "People");

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "People",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "stores 1 for Female, 0 for Male");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Patients",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
