using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    public partial class renamedAddressToAddressLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address2",
                table: "Schools",
                newName: "AddressLine2");

            migrationBuilder.RenameColumn(
                name: "Address1",
                table: "Schools",
                newName: "AddressLine1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddressLine2",
                table: "Schools",
                newName: "Address2");

            migrationBuilder.RenameColumn(
                name: "AddressLine1",
                table: "Schools",
                newName: "Address1");
        }
    }
}
