using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CustomEnums",
                columns: new[] { "Id", "EnumType", "Key", "Value" },
                values: new object[,]
                {
                    { 1, 0, "Poultry", "Fjerkræ" },
                    { 2, 0, "Seafood", "Skaldyr" },
                    { 3, 0, "RedMeat", "Oksekød" },
                    { 4, 0, "Pork", "Svinekød" },
                    { 5, 0, "SpicyFood", "Stærk mad" },
                    { 6, 0, "Cheese", "Ost" },
                    { 7, 0, "Pasta", "Pasta" },
                    { 8, 0, "Pizza", "Pizza" },
                    { 9, 0, "Vegetarian", "Vegetar" },
                    { 10, 0, "Salad", "Salat" },
                    { 11, 0, "Dessert", "Dessert" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CustomEnums",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
