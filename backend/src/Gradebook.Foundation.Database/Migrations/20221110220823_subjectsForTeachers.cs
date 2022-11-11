using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class subjectsForTeachers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubjectGuid",
                table: "Person",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherGuid",
                table: "Person",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Person_SubjectGuid",
                table: "Person",
                column: "SubjectGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Person_TeacherGuid",
                table: "Person",
                column: "TeacherGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Person_TeacherGuid",
                table: "Person",
                column: "TeacherGuid",
                principalTable: "Person",
                principalColumn: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Subjects_SubjectGuid",
                table: "Person",
                column: "SubjectGuid",
                principalTable: "Subjects",
                principalColumn: "Guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Person_TeacherGuid",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Subjects_SubjectGuid",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_SubjectGuid",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_TeacherGuid",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "SubjectGuid",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "TeacherGuid",
                table: "Person");
        }
    }
}
