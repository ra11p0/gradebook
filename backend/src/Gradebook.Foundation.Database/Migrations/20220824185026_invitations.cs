using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class invitations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_StudentGuid",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Teachers_TeacherGuid",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsAbsences_Students_StudentGuid",
                table: "StudentsAbsences");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersAbsences_Teachers_TeacherGuid",
                table: "TeachersAbsences");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "Person");

            migrationBuilder.AddColumn<Guid>(
                name: "ClassGuid",
                table: "Person",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Person",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupGuid",
                table: "Person",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "Guid");

            migrationBuilder.CreateTable(
                name: "SystemInvitations",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExprationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    InvitationCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsUsed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UsedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    InvitedPersonGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemInvitations", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_SystemInvitations_Person_CreatorGuid",
                        column: x => x.CreatorGuid,
                        principalTable: "Person",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemInvitations_Person_InvitedPersonGuid",
                        column: x => x.InvitedPersonGuid,
                        principalTable: "Person",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Person_ClassGuid",
                table: "Person",
                column: "ClassGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Person_GroupGuid",
                table: "Person",
                column: "GroupGuid");

            migrationBuilder.CreateIndex(
                name: "IX_SystemInvitations_CreatorGuid",
                table: "SystemInvitations",
                column: "CreatorGuid");

            migrationBuilder.CreateIndex(
                name: "IX_SystemInvitations_InvitedPersonGuid",
                table: "SystemInvitations",
                column: "InvitedPersonGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Person_StudentGuid",
                table: "Grades",
                column: "StudentGuid",
                principalTable: "Person",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Person_TeacherGuid",
                table: "Grades",
                column: "TeacherGuid",
                principalTable: "Person",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsAbsences_Person_StudentGuid",
                table: "StudentsAbsences",
                column: "StudentGuid",
                principalTable: "Person",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersAbsences_Person_TeacherGuid",
                table: "TeachersAbsences",
                column: "TeacherGuid",
                principalTable: "Person",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Person_StudentGuid",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Person_TeacherGuid",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Classes_ClassGuid",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Groups_GroupGuid",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsAbsences_Person_StudentGuid",
                table: "StudentsAbsences");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersAbsences_Person_TeacherGuid",
                table: "TeachersAbsences");

            migrationBuilder.DropTable(
                name: "SystemInvitations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
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
                name: "Discriminator",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "GroupGuid",
                table: "Person");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Teachers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "Guid");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClassGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GroupGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserGuid = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Students_Classes_ClassGuid",
                        column: x => x.ClassGuid,
                        principalTable: "Classes",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Groups_GroupGuid",
                        column: x => x.GroupGuid,
                        principalTable: "Groups",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassGuid",
                table: "Students",
                column: "ClassGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupGuid",
                table: "Students",
                column: "GroupGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_StudentGuid",
                table: "Grades",
                column: "StudentGuid",
                principalTable: "Students",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Teachers_TeacherGuid",
                table: "Grades",
                column: "TeacherGuid",
                principalTable: "Teachers",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsAbsences_Students_StudentGuid",
                table: "StudentsAbsences",
                column: "StudentGuid",
                principalTable: "Students",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersAbsences_Teachers_TeacherGuid",
                table: "TeachersAbsences",
                column: "TeacherGuid",
                principalTable: "Teachers",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
