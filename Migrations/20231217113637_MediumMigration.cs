using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class MediumMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CustomEnums",
                columns: new[] { "Id", "EnumType", "Key", "Value" },
                values: new object[,]
                {
                    { 12, 1, "RedWine", "Rødvin" },
                    { 13, 1, "WhiteWine", "Hvidvin" },
                    { 14, 1, "RoseWine", "Rosévin" },
                    { 15, 2, "Whiskey", "Whiskey" },
                    { 16, 2, "Vodka", "Vodka" },
                    { 17, 2, "Gin", "Gin" },
                    { 18, 2, "Rum", "Rom" },
                    { 19, 2, "Tequila", "Tequila" },
                    { 20, 2, "Liqueur", "Likør" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
