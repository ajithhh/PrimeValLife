using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeValLife.Core.Migrations
{
    /// <inheritdoc />
    public partial class null_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FName",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "Addresses");
        }
    }
}
