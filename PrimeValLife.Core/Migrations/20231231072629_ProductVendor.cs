using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeValLife.Core.Migrations
{
    /// <inheritdoc />
    public partial class ProductVendor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Addresses_AddressId",
                table: "Vendors");
            migrationBuilder.DropIndex(
               name: "IX_Vendors_AddressId",
               table: "Vendors"
               );
            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Vendors");
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Vendors"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Vendors_UserId_VendorId",
                table: "Vendors",
                columns: new[] { "UserId", "VendorId" }
                );
            migrationBuilder.AddColumn<bool>(
                name: "IsOnSale",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductDescription",
                table: "ProductPrimaryInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductLongDescription",
                table: "ProductPrimaryInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Products_VendorId",
                table: "Products",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Vendors_VendorId",
                table: "Products",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Users_UserId",
                table: "Vendors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Vendors_VendorId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Users_UserId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Products_VendorId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsOnSale",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "ProductPrimaryInfos");

            migrationBuilder.DropColumn(
                name: "ProductLongDescription",
                table: "ProductPrimaryInfos");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Vendors",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendors_UserId",
                table: "Vendors",
                newName: "IX_Vendors_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Addresses_AddressId",
                table: "Vendors",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
