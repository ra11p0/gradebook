﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class addedSchoolPropertyInClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SchoolGuid",
                table: "Classes",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SchoolGuid",
                table: "Classes",
                column: "SchoolGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Schools_SchoolGuid",
                table: "Classes",
                column: "SchoolGuid",
                principalTable: "Schools",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Schools_SchoolGuid",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_SchoolGuid",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "SchoolGuid",
                table: "Classes");
        }
    }
}
