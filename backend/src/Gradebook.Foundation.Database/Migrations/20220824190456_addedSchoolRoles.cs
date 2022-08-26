using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class addedSchoolRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "SystemInvitations");

            migrationBuilder.AddColumn<int>(
                name: "SchoolRole",
                table: "SystemInvitations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchoolRole",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolRole",
                table: "SystemInvitations");

            migrationBuilder.DropColumn(
                name: "SchoolRole",
                table: "Person");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "SystemInvitations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
