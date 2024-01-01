using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeValLife.Core.Migrations
{
    /// <inheritdoc />
    public partial class ProductPrimaryInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductPrimaryInfos_ProductId",
                table: "ProductPrimaryInfos",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPrimaryInfos_Products_ProductId",
                table: "ProductPrimaryInfos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPrimaryInfos_Products_ProductId",
                table: "ProductPrimaryInfos");

            migrationBuilder.DropIndex(
                name: "IX_ProductPrimaryInfos_ProductId",
                table: "ProductPrimaryInfos");
        }
    }
}
