using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ItemSpecification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Order_OrderId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_OrderId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "GuestId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderState",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Order",
                type: "double",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Items",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "ReservedQuantity",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<double>(type: "double", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLine_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderLine_Order_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Order_GuestId",
                table: "Order",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_ItemId",
                table: "OrderLine",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_PurchaseOrderId",
                table: "OrderLine",
                column: "PurchaseOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Users_GuestId",
                table: "Order",
                column: "GuestId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Users_GuestId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "OrderLine");

            migrationBuilder.DropIndex(
                name: "IX_Order_GuestId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderState",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ReservedQuantity",
                table: "Items");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Items",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderId",
                table: "Items",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Order_OrderId",
                table: "Items",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }
    }
}
