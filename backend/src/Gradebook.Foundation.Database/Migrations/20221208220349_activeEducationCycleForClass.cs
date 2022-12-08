using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class activeEducationCycleForClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationCycleSteps_Subjects_SubjectGuid",
                table: "EducationCycleSteps");

            migrationBuilder.DropIndex(
                name: "IX_EducationCycleSteps_SubjectGuid",
                table: "EducationCycleSteps");

            migrationBuilder.DropColumn(
                name: "SubjectGuid",
                table: "EducationCycleSteps");

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveEducationCycleGuid",
                table: "Classes",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ActiveEducationCycleGuid",
                table: "Classes",
                column: "ActiveEducationCycleGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_EducationCycles_ActiveEducationCycleGuid",
                table: "Classes",
                column: "ActiveEducationCycleGuid",
                principalTable: "EducationCycles",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_EducationCycles_ActiveEducationCycleGuid",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_ActiveEducationCycleGuid",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ActiveEducationCycleGuid",
                table: "Classes");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectGuid",
                table: "EducationCycleSteps",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycleSteps_SubjectGuid",
                table: "EducationCycleSteps",
                column: "SubjectGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationCycleSteps_Subjects_SubjectGuid",
                table: "EducationCycleSteps",
                column: "SubjectGuid",
                principalTable: "Subjects",
                principalColumn: "Guid");
        }
    }
}
