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
            var sqlFolderPath = GetSqlFolderPath();

            var seedData = new List<string>
            {
                { Path.Combine(sqlFolderPath, "countries.sql") },
                { Path.Combine(sqlFolderPath, "states.sql") },
                { Path.Combine(sqlFolderPath, "cities.sql") }
            };

            foreach (string sqlPath in seedData)
            {
                var sql = File.ReadAllText(sqlPath);
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

        private static string GetSqlFolderPath()
        {
            var workingDirectory = Environment.CurrentDirectory;
            var solutionDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName;

            if (solutionDirectory is null)
                throw new DirectoryNotFoundException("Unable to locate the solution directory.");

            var sqlFolder = Path.Combine(solutionDirectory, "scripts");

            return sqlFolder;
        }
    }
}
