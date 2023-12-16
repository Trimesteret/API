using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class PurchaseOrderChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Order",
                newName: "CustomerPhone");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Order",
                newName: "CustomerLastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Order",
                newName: "CustomerFirstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Order",
                newName: "CustomerEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerPhone",
                table: "Order",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "CustomerLastName",
                table: "Order",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "CustomerFirstName",
                table: "Order",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "CustomerEmail",
                table: "Order",
                newName: "Email");
        }
    }
}
