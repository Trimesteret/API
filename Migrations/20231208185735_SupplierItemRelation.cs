using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class SupplierItemRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemRelations_Suppliers_SupplierId",
                table: "ItemRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemRelations",
                table: "ItemRelations");

            migrationBuilder.RenameTable(
                name: "ItemRelations",
                newName: "SupplierItemRelations");

            migrationBuilder.RenameIndex(
                name: "IX_ItemRelations_SupplierId",
                table: "SupplierItemRelations",
                newName: "IX_SupplierItemRelations_SupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierItemRelations",
                table: "SupplierItemRelations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierItemRelations_Suppliers_SupplierId",
                table: "SupplierItemRelations",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierItemRelations_Suppliers_SupplierId",
                table: "SupplierItemRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierItemRelations",
                table: "SupplierItemRelations");

            migrationBuilder.RenameTable(
                name: "SupplierItemRelations",
                newName: "ItemRelations");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierItemRelations_SupplierId",
                table: "ItemRelations",
                newName: "IX_ItemRelations_SupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemRelations",
                table: "ItemRelations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemRelations_Suppliers_SupplierId",
                table: "ItemRelations",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
