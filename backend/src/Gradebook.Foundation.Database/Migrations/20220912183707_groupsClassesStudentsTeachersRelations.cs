using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class groupsClassesStudentsTeachersRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Classes_ClassGuid",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Groups_GroupGuid",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_ClassGuid",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_GroupGuid",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ClassGuid",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "GroupGuid",
                table: "Person");

            migrationBuilder.CreateTable(
                name: "ClassStudent",
                columns: table => new
                {
                    ClassesGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentsGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassStudent", x => new { x.ClassesGuid, x.StudentsGuid });
                    table.ForeignKey(
                        name: "FK_ClassStudent_Classes_ClassesGuid",
                        column: x => x.ClassesGuid,
                        principalTable: "Classes",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassStudent_Person_StudentsGuid",
                        column: x => x.StudentsGuid,
                        principalTable: "Person",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClassTeacher",
                columns: table => new
                {
                    ClassesGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeachersGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTeacher", x => new { x.ClassesGuid, x.TeachersGuid });
                    table.ForeignKey(
                        name: "FK_ClassTeacher_Classes_ClassesGuid",
                        column: x => x.ClassesGuid,
                        principalTable: "Classes",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassTeacher_Person_TeachersGuid",
                        column: x => x.TeachersGuid,
                        principalTable: "Person",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GroupStudent",
                columns: table => new
                {
                    GroupsGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentsGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupStudent", x => new { x.GroupsGuid, x.StudentsGuid });
                    table.ForeignKey(
                        name: "FK_GroupStudent_Groups_GroupsGuid",
                        column: x => x.GroupsGuid,
                        principalTable: "Groups",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupStudent_Person_StudentsGuid",
                        column: x => x.StudentsGuid,
                        principalTable: "Person",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GroupTeacher",
                columns: table => new
                {
                    GroupsGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeachersGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTeacher", x => new { x.GroupsGuid, x.TeachersGuid });
                    table.ForeignKey(
                        name: "FK_GroupTeacher_Groups_GroupsGuid",
                        column: x => x.GroupsGuid,
                        principalTable: "Groups",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTeacher_Person_TeachersGuid",
                        column: x => x.TeachersGuid,
                        principalTable: "Person",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ClassStudent_StudentsGuid",
                table: "ClassStudent",
                column: "StudentsGuid");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeacher_TeachersGuid",
                table: "ClassTeacher",
                column: "TeachersGuid");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStudent_StudentsGuid",
                table: "GroupStudent",
                column: "StudentsGuid");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTeacher_TeachersGuid",
                table: "GroupTeacher",
                column: "TeachersGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassStudent");

            migrationBuilder.DropTable(
                name: "ClassTeacher");

            migrationBuilder.DropTable(
                name: "GroupStudent");

            migrationBuilder.DropTable(
                name: "GroupTeacher");

            migrationBuilder.AddColumn<Guid>(
                name: "ClassGuid",
                table: "Person",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupGuid",
                table: "Person",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Person_ClassGuid",
                table: "Person",
                column: "ClassGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Person_GroupGuid",
                table: "Person",
                column: "GroupGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Classes_ClassGuid",
                table: "Person",
                column: "ClassGuid",
                principalTable: "Classes",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Groups_GroupGuid",
                table: "Person",
                column: "GroupGuid",
                principalTable: "Groups",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
