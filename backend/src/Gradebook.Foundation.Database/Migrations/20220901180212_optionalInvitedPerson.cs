using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class optionalInvitedPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemInvitations_Person_InvitedPersonGuid",
                table: "SystemInvitations");

            migrationBuilder.AlterColumn<Guid>(
                name: "InvitedPersonGuid",
                table: "SystemInvitations",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemInvitations_Person_InvitedPersonGuid",
                table: "SystemInvitations",
                column: "InvitedPersonGuid",
                principalTable: "Person",
                principalColumn: "Guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemInvitations_Person_InvitedPersonGuid",
                table: "SystemInvitations");

            migrationBuilder.AlterColumn<Guid>(
                name: "InvitedPersonGuid",
                table: "SystemInvitations",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemInvitations_Person_InvitedPersonGuid",
                table: "SystemInvitations",
                column: "InvitedPersonGuid",
                principalTable: "Person",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
