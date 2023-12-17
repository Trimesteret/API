using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class LilleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WineType",
                table: "Items",
                newName: "Wine_Year");

            migrationBuilder.AddColumn<int>(
                name: "LiquorTypeId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WineTypeId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Wine_AlcoholPercentage",
                table: "Items",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Wine_Volume",
                table: "Items",
                type: "double",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_LiquorTypeId",
                table: "Items",
                column: "LiquorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_WineTypeId",
                table: "Items",
                column: "WineTypeId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_CustomEnums_LiquorTypeId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_CustomEnums_WineTypeId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_LiquorTypeId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_WineTypeId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "LiquorTypeId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "WineTypeId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Wine_AlcoholPercentage",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Wine_Volume",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "Wine_Year",
                table: "Items",
                newName: "WineType");
        }
    }
}
