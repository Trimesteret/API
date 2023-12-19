using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddressLineUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_DeliveryAddressId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddressId",
                table: "Order",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_DeliveryAddressId",
                table: "Order",
                newName: "IX_Order_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_AddressId",
                table: "Order",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_AddressId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Order",
                newName: "DeliveryAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AddressId",
                table: "Order",
                newName: "IX_Order_DeliveryAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_DeliveryAddressId",
                table: "Order",
                column: "DeliveryAddressId",
                principalTable: "Address",
                principalColumn: "Id");
        }
    }
}
