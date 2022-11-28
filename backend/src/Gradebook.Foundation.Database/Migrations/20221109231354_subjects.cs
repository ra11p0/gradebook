using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class subjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SchoolGuid",
                table: "Subjects",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SchoolGuid",
                table: "Subjects",
                column: "SchoolGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Schools_SchoolGuid",
                table: "Subjects",
                column: "SchoolGuid",
                principalTable: "Schools",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Schools_SchoolGuid",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SchoolGuid",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SchoolGuid",
                table: "Subjects");
        }
    }
}
