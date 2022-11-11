using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class subjectsForTeachers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "SubjectTeacher",
                columns: table => new
                {
                    SubjectsGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeachersGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTeacher", x => new { x.SubjectsGuid, x.TeachersGuid });
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_Person_TeachersGuid",
                        column: x => x.TeachersGuid,
                        principalTable: "Person",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_Subjects_SubjectsGuid",
                        column: x => x.SubjectsGuid,
                        principalTable: "Subjects",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_TeachersGuid",
                table: "SubjectTeacher",
                column: "TeachersGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectTeacher");

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
    }
}
