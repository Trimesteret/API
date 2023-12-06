using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InboundOrderId",
                table: "OrderLine",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Order",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseOrderState",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Order",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Supplier",
                table: "Order",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_InboundOrderId",
                table: "OrderLine",
                column: "InboundOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLine_Order_InboundOrderId",
                table: "OrderLine",
                column: "InboundOrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLine_Order_InboundOrderId",
                table: "OrderLine");

            migrationBuilder.DropIndex(
                name: "IX_OrderLine_InboundOrderId",
                table: "OrderLine");

            migrationBuilder.DropColumn(
                name: "InboundOrderId",
                table: "OrderLine");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "Order");

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Order",
                type: "double",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseOrderState",
                table: "Order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
