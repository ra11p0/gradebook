using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class changedTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationCycleStepInstance_EducationCycleInstances_Education~",
                table: "EducationCycleStepInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationCycleStepInstance_EducationCycleSteps_EducationCycl~",
                table: "EducationCycleStepInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationCycleStepSubjectInstance_EducationCycleStepInstance~",
                table: "EducationCycleStepSubjectInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationCycleStepSubjectInstance_Person_AssignedTeacherGuid",
                table: "EducationCycleStepSubjectInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_EducationCycleStepSubjectInstance_EducationCycleStep~",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationCycleStepSubjectInstance",
                table: "EducationCycleStepSubjectInstance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationCycleStepInstance",
                table: "EducationCycleStepInstance");

            migrationBuilder.RenameTable(
                name: "EducationCycleStepSubjectInstance",
                newName: "EducationCycleStepSubjectInstances");

            migrationBuilder.RenameTable(
                name: "EducationCycleStepInstance",
                newName: "EducationCycleStepInstances");

            migrationBuilder.RenameIndex(
                name: "IX_EducationCycleStepSubjectInstance_EducationCycleStepInstance~",
                table: "EducationCycleStepSubjectInstances",
                newName: "IX_EducationCycleStepSubjectInstances_EducationCycleStepInstanc~");

            migrationBuilder.RenameIndex(
                name: "IX_EducationCycleStepSubjectInstance_AssignedTeacherGuid",
                table: "EducationCycleStepSubjectInstances",
                newName: "IX_EducationCycleStepSubjectInstances_AssignedTeacherGuid");

            migrationBuilder.RenameIndex(
                name: "IX_EducationCycleStepInstance_EducationCycleStepGuid",
                table: "EducationCycleStepInstances",
                newName: "IX_EducationCycleStepInstances_EducationCycleStepGuid");

            migrationBuilder.RenameIndex(
                name: "IX_EducationCycleStepInstance_EducationCycleInstanceGuid",
                table: "EducationCycleStepInstances",
                newName: "IX_EducationCycleStepInstances_EducationCycleInstanceGuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationCycleStepSubjectInstances",
                table: "EducationCycleStepSubjectInstances",
                column: "Guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationCycleStepInstances",
                table: "EducationCycleStepInstances",
                column: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationCycleStepInstances_EducationCycleInstances_Educatio~",
                table: "EducationCycleStepInstances",
                column: "EducationCycleInstanceGuid",
                principalTable: "EducationCycleInstances",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationCycleStepInstances_EducationCycleSteps_EducationCyc~",
                table: "EducationCycleStepInstances",
                column: "EducationCycleStepGuid",
                principalTable: "EducationCycleSteps",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationCycleStepSubjectInstances_EducationCycleStepInstanc~",
                table: "EducationCycleStepSubjectInstances",
                column: "EducationCycleStepInstanceGuid",
                principalTable: "EducationCycleStepInstances",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationCycleStepSubjectInstances_Person_AssignedTeacherGuid",
                table: "EducationCycleStepSubjectInstances",
                column: "AssignedTeacherGuid",
                principalTable: "Person",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_EducationCycleStepSubjectInstances_EducationCycleSte~",
                table: "Lessons",
                column: "EducationCycleStepSubjectInstanceGuid",
                principalTable: "EducationCycleStepSubjectInstances",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationCycleStepInstances_EducationCycleInstances_Educatio~",
                table: "EducationCycleStepInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationCycleStepInstances_EducationCycleSteps_EducationCyc~",
                table: "EducationCycleStepInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationCycleStepSubjectInstances_EducationCycleStepInstanc~",
                table: "EducationCycleStepSubjectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationCycleStepSubjectInstances_Person_AssignedTeacherGuid",
                table: "EducationCycleStepSubjectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_EducationCycleStepSubjectInstances_EducationCycleSte~",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationCycleStepSubjectInstances",
                table: "EducationCycleStepSubjectInstances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationCycleStepInstances",
                table: "EducationCycleStepInstances");

            migrationBuilder.RenameTable(
                name: "EducationCycleStepSubjectInstances",
                newName: "EducationCycleStepSubjectInstance");

            migrationBuilder.RenameTable(
                name: "EducationCycleStepInstances",
                newName: "EducationCycleStepInstance");

            migrationBuilder.RenameIndex(
                name: "IX_EducationCycleStepSubjectInstances_EducationCycleStepInstanc~",
                table: "EducationCycleStepSubjectInstance",
                newName: "IX_EducationCycleStepSubjectInstance_EducationCycleStepInstance~");

            migrationBuilder.RenameIndex(
                name: "IX_EducationCycleStepSubjectInstances_AssignedTeacherGuid",
                table: "EducationCycleStepSubjectInstance",
                newName: "IX_EducationCycleStepSubjectInstance_AssignedTeacherGuid");

            migrationBuilder.RenameIndex(
                name: "IX_EducationCycleStepInstances_EducationCycleStepGuid",
                table: "EducationCycleStepInstance",
                newName: "IX_EducationCycleStepInstance_EducationCycleStepGuid");

            migrationBuilder.RenameIndex(
                name: "IX_EducationCycleStepInstances_EducationCycleInstanceGuid",
                table: "EducationCycleStepInstance",
                newName: "IX_EducationCycleStepInstance_EducationCycleInstanceGuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationCycleStepSubjectInstance",
                table: "EducationCycleStepSubjectInstance",
                column: "Guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationCycleStepInstance",
                table: "EducationCycleStepInstance",
                column: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationCycleStepInstance_EducationCycleInstances_Education~",
                table: "EducationCycleStepInstance",
                column: "EducationCycleInstanceGuid",
                principalTable: "EducationCycleInstances",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationCycleStepInstance_EducationCycleSteps_EducationCycl~",
                table: "EducationCycleStepInstance",
                column: "EducationCycleStepGuid",
                principalTable: "EducationCycleSteps",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationCycleStepSubjectInstance_EducationCycleStepInstance~",
                table: "EducationCycleStepSubjectInstance",
                column: "EducationCycleStepInstanceGuid",
                principalTable: "EducationCycleStepInstance",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationCycleStepSubjectInstance_Person_AssignedTeacherGuid",
                table: "EducationCycleStepSubjectInstance",
                column: "AssignedTeacherGuid",
                principalTable: "Person",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_EducationCycleStepSubjectInstance_EducationCycleStep~",
                table: "Lessons",
                column: "EducationCycleStepSubjectInstanceGuid",
                principalTable: "EducationCycleStepSubjectInstance",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
