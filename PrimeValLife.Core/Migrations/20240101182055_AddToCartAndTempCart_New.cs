using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeValLife.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddToCartAndTempCart_New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempCarts",
                columns: table => new
                {
                    TempCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempCarts", x => x.TempCartId);
                });

            migrationBuilder.CreateTable(
                name: "TempCartItems",
                columns: table => new
                {
                    TempCartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TempCartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempCartItems", x => x.TempCartItemId);
                    table.ForeignKey(
                        name: "FK_TempCartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempCartItems_TempCarts_TempCartId",
                        column: x => x.TempCartId,
                        principalTable: "TempCarts",
                        principalColumn: "TempCartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TempCartItems_ProductId",
                table: "TempCartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TempCartItems_TempCartId",
                table: "TempCartItems",
                column: "TempCartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempCartItems");

            migrationBuilder.DropTable(
                name: "TempCarts");
        }
    }
}
