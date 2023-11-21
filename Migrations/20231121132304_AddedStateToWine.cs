using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddedStateToWine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AlcoholPercentage",
                table: "Items",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Items",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GrapeSort",
                table: "Items",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Items",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SuitableFor",
                table: "Items",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TastingNotes",
                table: "Items",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Volume",
                table: "Items",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WineType",
                table: "Items",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Winery",
                table: "Items",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Items",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlcoholPercentage",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GrapeSort",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SuitableFor",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TastingNotes",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "WineType",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Winery",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Items");
        }
    }
}
