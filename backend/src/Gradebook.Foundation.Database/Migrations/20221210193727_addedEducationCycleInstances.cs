using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class addedEducationCycleInstances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateSince",
                table: "Lessons",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUntil",
                table: "Lessons",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationCycleStepSubjectInstanceGuid",
                table: "Lessons",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "StartingPersonGuid",
                table: "Lessons",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "EducationCycleInstances",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClassGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EducationCycleGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateSince = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateUntil = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationCycleInstances", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_EducationCycleInstances_Classes_ClassGuid",
                        column: x => x.ClassGuid,
                        principalTable: "Classes",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationCycleInstances_EducationCycles_EducationCycleGuid",
                        column: x => x.EducationCycleGuid,
                        principalTable: "EducationCycles",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationCycleInstances_Person_CreatorGuid",
                        column: x => x.CreatorGuid,
                        principalTable: "Person",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EducationCycleStepInstance",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EducationCycleStepGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EducationCycleInstanceGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateSince = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateUntil = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationCycleStepInstance", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_EducationCycleStepInstance_EducationCycleInstances_Education~",
                        column: x => x.EducationCycleInstanceGuid,
                        principalTable: "EducationCycleInstances",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationCycleStepInstance_EducationCycleSteps_EducationCycl~",
                        column: x => x.EducationCycleStepGuid,
                        principalTable: "EducationCycleSteps",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EducationCycleStepSubjectInstance",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AssignedTeacherGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EducationCycleStepInstanceGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationCycleStepSubjectInstance", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_EducationCycleStepSubjectInstance_EducationCycleStepInstance~",
                        column: x => x.EducationCycleStepInstanceGuid,
                        principalTable: "EducationCycleStepInstance",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationCycleStepSubjectInstance_Person_AssignedTeacherGuid",
                        column: x => x.AssignedTeacherGuid,
                        principalTable: "Person",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_EducationCycleStepSubjectInstanceGuid",
                table: "Lessons",
                column: "EducationCycleStepSubjectInstanceGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_StartingPersonGuid",
                table: "Lessons",
                column: "StartingPersonGuid");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycleInstances_ClassGuid",
                table: "EducationCycleInstances",
                column: "ClassGuid");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycleInstances_CreatorGuid",
                table: "EducationCycleInstances",
                column: "CreatorGuid");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycleInstances_EducationCycleGuid",
                table: "EducationCycleInstances",
                column: "EducationCycleGuid");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycleStepInstance_EducationCycleInstanceGuid",
                table: "EducationCycleStepInstance",
                column: "EducationCycleInstanceGuid");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycleStepInstance_EducationCycleStepGuid",
                table: "EducationCycleStepInstance",
                column: "EducationCycleStepGuid");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycleStepSubjectInstance_AssignedTeacherGuid",
                table: "EducationCycleStepSubjectInstance",
                column: "AssignedTeacherGuid");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycleStepSubjectInstance_EducationCycleStepInstance~",
                table: "EducationCycleStepSubjectInstance",
                column: "EducationCycleStepInstanceGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_EducationCycleStepSubjectInstance_EducationCycleStep~",
                table: "Lessons",
                column: "EducationCycleStepSubjectInstanceGuid",
                principalTable: "EducationCycleStepSubjectInstance",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Person_StartingPersonGuid",
                table: "Lessons",
                column: "StartingPersonGuid",
                principalTable: "Person",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_EducationCycleStepSubjectInstance_EducationCycleStep~",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Person_StartingPersonGuid",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "EducationCycleStepSubjectInstance");

            migrationBuilder.DropTable(
                name: "EducationCycleStepInstance");

            migrationBuilder.DropTable(
                name: "EducationCycleInstances");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_EducationCycleStepSubjectInstanceGuid",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_StartingPersonGuid",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "DateSince",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "DateUntil",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "EducationCycleStepSubjectInstanceGuid",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "StartingPersonGuid",
                table: "Lessons");
        }
    }
}
