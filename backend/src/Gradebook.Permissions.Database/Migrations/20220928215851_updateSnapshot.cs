using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Permissions.Database.Migrations
{
    public partial class updateSnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
