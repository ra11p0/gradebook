using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class modifiedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EducationCycleStepSubjectGuid",
                table: "EducationCycleStepSubjectInstances",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCycleStepSubjectInstances_EducationCycleStepSubject~",
                table: "EducationCycleStepSubjectInstances",
                column: "EducationCycleStepSubjectGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationCycleStepSubjectInstances_EducationCycleStepSubject~",
                table: "EducationCycleStepSubjectInstances",
                column: "EducationCycleStepSubjectGuid",
                principalTable: "EducationCycleStepSubjects",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationCycleStepSubjectInstances_EducationCycleStepSubject~",
                table: "EducationCycleStepSubjectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EducationCycleStepSubjectInstances_EducationCycleStepSubject~",
                table: "EducationCycleStepSubjectInstances");

            migrationBuilder.DropColumn(
                name: "EducationCycleStepSubjectGuid",
                table: "EducationCycleStepSubjectInstances");
        }
    }
}
