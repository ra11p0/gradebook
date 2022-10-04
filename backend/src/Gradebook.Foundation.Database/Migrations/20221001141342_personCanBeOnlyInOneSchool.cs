using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class personCanBeOnlyInOneSchool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonSchool");

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolGuid",
                table: "Person",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Person_SchoolGuid",
                table: "Person",
                column: "SchoolGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Schools_SchoolGuid",
                table: "Person",
                column: "SchoolGuid",
                principalTable: "Schools",
                principalColumn: "Guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Schools_SchoolGuid",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_SchoolGuid",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "SchoolGuid",
                table: "Person");

            migrationBuilder.CreateTable(
                name: "PersonSchool",
                columns: table => new
                {
                    PeopleGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SchoolsGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonSchool", x => new { x.PeopleGuid, x.SchoolsGuid });
                    table.ForeignKey(
                        name: "FK_PersonSchool_Person_PeopleGuid",
                        column: x => x.PeopleGuid,
                        principalTable: "Person",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonSchool_Schools_SchoolsGuid",
                        column: x => x.SchoolsGuid,
                        principalTable: "Schools",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSchool_SchoolsGuid",
                table: "PersonSchool",
                column: "SchoolsGuid");
        }
    }
}
