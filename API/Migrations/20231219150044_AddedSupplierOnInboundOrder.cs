using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddedSupplierOnInboundOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Suppliers_SupplierId",
                table: "Order");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Suppliers_SupplierId",
                table: "Order",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Suppliers_SupplierId",
                table: "Order");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Suppliers_SupplierId",
                table: "Order",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }
    }
}
