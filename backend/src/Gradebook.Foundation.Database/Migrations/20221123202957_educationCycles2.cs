using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class educationCycles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Schools_SchoolGuid",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "EducationCycleStepSubject");

            migrationBuilder.AlterColumn<Guid>(
                name: "SchoolGuid",
                table: "Subjects",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "EducationCycleSteps",
                keyColumn: "Name",
                keyValue: null,
                column: "Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EducationCycleSteps",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectGuid",
                table: "EducationCycleSteps",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "EducationCycles",
                keyColumn: "Name",
                keyValue: null,
                column: "Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EducationCycles",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EducationCycleStepSubjects",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubjectGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EducationCycleStepGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    HoursInStep = table.Column<int>(type: "int", nullable: false),
                    IsMandatory = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    GroupsAllowed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationCycleStepSubjects", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_EducationCycleStepSubjects_EducationCycleSteps_EducationCycl~",
                        column: x => x.EducationCycleStepGuid,
                        principalTable: "EducationCycleSteps",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationCycleStepSubjects_Subjects_SubjectGuid",
                        column: x => x.SubjectGuid,
                        principalTable: "Subjects",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycleSteps_SubjectGuid",
                table: "EducationCycleSteps",
                column: "SubjectGuid");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycleStepSubjects_EducationCycleStepGuid",
                table: "EducationCycleStepSubjects",
                column: "EducationCycleStepGuid");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycleStepSubjects_SubjectGuid",
                table: "EducationCycleStepSubjects",
                column: "SubjectGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationCycleSteps_Subjects_SubjectGuid",
                table: "EducationCycleSteps",
                column: "SubjectGuid",
                principalTable: "Subjects",
                principalColumn: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Schools_SchoolGuid",
                table: "Subjects",
                column: "SchoolGuid",
                principalTable: "Schools",
                principalColumn: "Guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationCycleSteps_Subjects_SubjectGuid",
                table: "EducationCycleSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Schools_SchoolGuid",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "EducationCycleStepSubjects");

            migrationBuilder.DropIndex(
                name: "IX_EducationCycleSteps_SubjectGuid",
                table: "EducationCycleSteps");

            migrationBuilder.DropColumn(
                name: "SubjectGuid",
                table: "EducationCycleSteps");

            migrationBuilder.AlterColumn<Guid>(
                name: "SchoolGuid",
                table: "Subjects",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EducationCycleSteps",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EducationCycles",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EducationCycleStepSubject",
                columns: table => new
                {
                    EducationCycleStepsGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubjectsGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationCycleStepSubject", x => new { x.EducationCycleStepsGuid, x.SubjectsGuid });
                    table.ForeignKey(
                        name: "FK_EducationCycleStepSubject_EducationCycleSteps_EducationCycle~",
                        column: x => x.EducationCycleStepsGuid,
                        principalTable: "EducationCycleSteps",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationCycleStepSubject_Subjects_SubjectsGuid",
                        column: x => x.SubjectsGuid,
                        principalTable: "Subjects",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycleStepSubject_SubjectsGuid",
                table: "EducationCycleStepSubject",
                column: "SubjectsGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Schools_SchoolGuid",
                table: "Subjects",
                column: "SchoolGuid",
                principalTable: "Schools",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
