using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class removedIdFromDatabaseModelBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "TeachersAbsences");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SystemInvitations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentsAbsences");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Classes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TeachersAbsences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SystemInvitations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StudentsAbsences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Schools",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Positions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
