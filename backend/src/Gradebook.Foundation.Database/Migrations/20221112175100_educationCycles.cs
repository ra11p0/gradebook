using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class educationCycles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EducationCycles",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SchoolGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatorGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationCycles", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_EducationCycles_Person_CreatorGuid",
                        column: x => x.CreatorGuid,
                        principalTable: "Person",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationCycles_Schools_SchoolGuid",
                        column: x => x.SchoolGuid,
                        principalTable: "Schools",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EducationCycleSteps",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    EducationCycleGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationCycleSteps", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_EducationCycleSteps_EducationCycles_EducationCycleGuid",
                        column: x => x.EducationCycleGuid,
                        principalTable: "EducationCycles",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "IX_EducationCycles_CreatorGuid",
                table: "EducationCycles",
                column: "CreatorGuid");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycles_SchoolGuid",
                table: "EducationCycles",
                column: "SchoolGuid");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycleSteps_EducationCycleGuid",
                table: "EducationCycleSteps",
                column: "EducationCycleGuid");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycleStepSubject_SubjectsGuid",
                table: "EducationCycleStepSubject",
                column: "SubjectsGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationCycleStepSubject");

            migrationBuilder.DropTable(
                name: "EducationCycleSteps");

            migrationBuilder.DropTable(
                name: "EducationCycles");
        }
    }
}
