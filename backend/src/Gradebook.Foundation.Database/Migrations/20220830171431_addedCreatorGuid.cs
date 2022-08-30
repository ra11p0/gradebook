using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class addedCreatorGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatorGuid",
                table: "Person",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "Teacher_CreatorGuid",
                table: "Person",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Person_CreatorGuid",
                table: "Person",
                column: "CreatorGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Person_Teacher_CreatorGuid",
                table: "Person",
                column: "Teacher_CreatorGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Person_CreatorGuid",
                table: "Person",
                column: "CreatorGuid",
                principalTable: "Person",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Person_Teacher_CreatorGuid",
                table: "Person",
                column: "Teacher_CreatorGuid",
                principalTable: "Person",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Person_CreatorGuid",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Person_Teacher_CreatorGuid",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_CreatorGuid",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_Teacher_CreatorGuid",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "CreatorGuid",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Teacher_CreatorGuid",
                table: "Person");
        }
    }
}
