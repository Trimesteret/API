using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCustomEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomEnums_EnumType_EnumTypeId",
                table: "CustomEnums");

            migrationBuilder.DropTable(
                name: "EnumType");

            migrationBuilder.DropIndex(
                name: "IX_CustomEnums_EnumTypeId",
                table: "CustomEnums");

            migrationBuilder.DropColumn(
                name: "EnumTypeId",
                table: "CustomEnums");

            migrationBuilder.AddColumn<int>(
                name: "EnumType",
                table: "CustomEnums",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnumType",
                table: "CustomEnums");

            migrationBuilder.AddColumn<int>(
                name: "EnumTypeId",
                table: "CustomEnums",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EnumType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EnumName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnumType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CustomEnums_EnumTypeId",
                table: "CustomEnums",
                column: "EnumTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomEnums_EnumType_EnumTypeId",
                table: "CustomEnums",
                column: "EnumTypeId",
                principalTable: "EnumType",
                principalColumn: "Id");
        }
    }
}
