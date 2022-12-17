using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class educationCycleInstanceFinishedStartedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Started",
                table: "EducationCycleStepInstances");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedDate",
                table: "EducationCycleStepInstances",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartedDate",
                table: "EducationCycleStepInstances",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedDate",
                table: "EducationCycleStepInstances");

            migrationBuilder.DropColumn(
                name: "StartedDate",
                table: "EducationCycleStepInstances");

            migrationBuilder.AddColumn<bool>(
                name: "Started",
                table: "EducationCycleStepInstances",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
