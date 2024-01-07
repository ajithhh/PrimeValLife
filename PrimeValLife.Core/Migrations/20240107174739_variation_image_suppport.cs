using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeValLife.Core.Migrations
{
    /// <inheritdoc />
    public partial class variation_image_suppport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductVariationId",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductVariationId",
                table: "ProductImages",
                column: "ProductVariationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_ProductVariation_ProductVariationId",
                table: "ProductImages",
                column: "ProductVariationId",
                principalTable: "ProductVariation",
                principalColumn: "ProductVariationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_ProductVariation_ProductVariationId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductVariationId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ProductVariationId",
                table: "ProductImages");
        }
    }
}
