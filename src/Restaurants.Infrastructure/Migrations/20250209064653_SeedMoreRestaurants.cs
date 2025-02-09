using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreRestaurants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            var path = dirPath + @$"/Seeders/INSERT_Restaurants.sql";

            var exists = File.Exists(path);

            if (!exists)
                throw new InvalidOperationException($"SQL file doesn't exists, {path}");

            var sql = File.ReadAllText(path);

            if (sql == null)
                throw new InvalidOperationException($"SQL file doesnt have any SQL");

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
