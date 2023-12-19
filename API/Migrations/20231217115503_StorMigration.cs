using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class StorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_CustomEnums_LiquorTypeId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_CustomEnums_WineTypeId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "WineTypeId",
                table: "Items",
                newName: "WineTypeEnumId");

            migrationBuilder.RenameColumn(
                name: "LiquorTypeId",
                table: "Items",
                newName: "LiquorTypeEnumId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_WineTypeId",
                table: "Items",
                newName: "IX_Items_WineTypeEnumId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_LiquorTypeId",
                table: "Items",
                newName: "IX_Items_LiquorTypeEnumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_CustomEnums_LiquorTypeEnumId",
                table: "Items",
                column: "LiquorTypeEnumId",
                principalTable: "CustomEnums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_CustomEnums_WineTypeEnumId",
                table: "Items",
                column: "WineTypeEnumId",
                principalTable: "CustomEnums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_CustomEnums_LiquorTypeEnumId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_CustomEnums_WineTypeEnumId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "WineTypeEnumId",
                table: "Items",
                newName: "WineTypeId");

            migrationBuilder.RenameColumn(
                name: "LiquorTypeEnumId",
                table: "Items",
                newName: "LiquorTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_WineTypeEnumId",
                table: "Items",
                newName: "IX_Items_WineTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_LiquorTypeEnumId",
                table: "Items",
                newName: "IX_Items_LiquorTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_CustomEnums_LiquorTypeId",
                table: "Items",
                column: "LiquorTypeId",
                principalTable: "CustomEnums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_CustomEnums_WineTypeId",
                table: "Items",
                column: "WineTypeId",
                principalTable: "CustomEnums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
