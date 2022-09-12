using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class extendedClassesAndGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeacher_Classes_ClassesGuid",
                table: "ClassTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeacher_Person_TeachersGuid",
                table: "ClassTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Classes_ClassGuid",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeacher_Groups_GroupsGuid",
                table: "GroupTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeacher_Person_TeachersGuid",
                table: "GroupTeacher");

            migrationBuilder.DropIndex(
                name: "IX_Groups_ClassGuid",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ClassGuid",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "TeachersGuid",
                table: "GroupTeacher",
                newName: "OwnersTeachersGuid");

            migrationBuilder.RenameColumn(
                name: "GroupsGuid",
                table: "GroupTeacher",
                newName: "OwnedGroupsGuid");

            migrationBuilder.RenameIndex(
                name: "IX_GroupTeacher_TeachersGuid",
                table: "GroupTeacher",
                newName: "IX_GroupTeacher_OwnersTeachersGuid");

            migrationBuilder.RenameColumn(
                name: "TeachersGuid",
                table: "ClassTeacher",
                newName: "OwnersTeachersGuid");

            migrationBuilder.RenameColumn(
                name: "ClassesGuid",
                table: "ClassTeacher",
                newName: "OwnedClassesGuid");

            migrationBuilder.RenameIndex(
                name: "IX_ClassTeacher_TeachersGuid",
                table: "ClassTeacher",
                newName: "IX_ClassTeacher_OwnersTeachersGuid");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Groups",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Groups",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Classes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Classes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassTeacher_Classes_OwnedClassesGuid",
                table: "ClassTeacher",
                column: "OwnedClassesGuid",
                principalTable: "Classes",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassTeacher_Person_OwnersTeachersGuid",
                table: "ClassTeacher",
                column: "OwnersTeachersGuid",
                principalTable: "Person",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeacher_Groups_OwnedGroupsGuid",
                table: "GroupTeacher",
                column: "OwnedGroupsGuid",
                principalTable: "Groups",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeacher_Person_OwnersTeachersGuid",
                table: "GroupTeacher",
                column: "OwnersTeachersGuid",
                principalTable: "Person",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeacher_Classes_OwnedClassesGuid",
                table: "ClassTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeacher_Person_OwnersTeachersGuid",
                table: "ClassTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeacher_Groups_OwnedGroupsGuid",
                table: "GroupTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeacher_Person_OwnersTeachersGuid",
                table: "GroupTeacher");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "OwnersTeachersGuid",
                table: "GroupTeacher",
                newName: "TeachersGuid");

            migrationBuilder.RenameColumn(
                name: "OwnedGroupsGuid",
                table: "GroupTeacher",
                newName: "GroupsGuid");

            migrationBuilder.RenameIndex(
                name: "IX_GroupTeacher_OwnersTeachersGuid",
                table: "GroupTeacher",
                newName: "IX_GroupTeacher_TeachersGuid");

            migrationBuilder.RenameColumn(
                name: "OwnersTeachersGuid",
                table: "ClassTeacher",
                newName: "TeachersGuid");

            migrationBuilder.RenameColumn(
                name: "OwnedClassesGuid",
                table: "ClassTeacher",
                newName: "ClassesGuid");

            migrationBuilder.RenameIndex(
                name: "IX_ClassTeacher_OwnersTeachersGuid",
                table: "ClassTeacher",
                newName: "IX_ClassTeacher_TeachersGuid");

            migrationBuilder.AddColumn<Guid>(
                name: "ClassGuid",
                table: "Groups",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ClassGuid",
                table: "Groups",
                column: "ClassGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassTeacher_Classes_ClassesGuid",
                table: "ClassTeacher",
                column: "ClassesGuid",
                principalTable: "Classes",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassTeacher_Person_TeachersGuid",
                table: "ClassTeacher",
                column: "TeachersGuid",
                principalTable: "Person",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Classes_ClassGuid",
                table: "Groups",
                column: "ClassGuid",
                principalTable: "Classes",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeacher_Groups_GroupsGuid",
                table: "GroupTeacher",
                column: "GroupsGuid",
                principalTable: "Groups",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeacher_Person_TeachersGuid",
                table: "GroupTeacher",
                column: "TeachersGuid",
                principalTable: "Person",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
