using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeValLife.Core.Migrations
{
    /// <inheritdoc />
    public partial class variation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariation_Products_ProductId",
                table: "ProductVariation");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductVariation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariation_Products_ProductId",
                table: "ProductVariation",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariation_Products_ProductId",
                table: "ProductVariation");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductVariation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariation_Products_ProductId",
                table: "ProductVariation",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }
    }
}
