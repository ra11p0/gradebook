using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class currentClassInStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentClassGuid",
                table: "Person",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Person_CurrentClassGuid",
                table: "Person",
                column: "CurrentClassGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Classes_CurrentClassGuid",
                table: "Person",
                column: "CurrentClassGuid",
                principalTable: "Classes",
                principalColumn: "Guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Classes_CurrentClassGuid",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_CurrentClassGuid",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "CurrentClassGuid",
                table: "Person");
        }
    }
}
