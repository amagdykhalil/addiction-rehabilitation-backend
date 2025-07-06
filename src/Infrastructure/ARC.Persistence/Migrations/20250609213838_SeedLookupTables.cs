using ARC.Persistence.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARC.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedLookupTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (SqlMigrationHelper.IsTesting())
                return;

            foreach (var file in new[] { "countries.sql", "states.sql", "cities.sql" })
            {
                var sql = SqlMigrationHelper.LoadSql(file);
                migrationBuilder.Sql(sql);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Countries");
            migrationBuilder.Sql("Delete from States");
            migrationBuilder.Sql("Delete from Cities");
        }
    }
}
