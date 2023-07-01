using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Sv.Migrations
{
    /// <inheritdoc />
    public partial class addingstringproductname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Products_ProductNameId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_ProductNameId",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "ProductNameId",
                table: "Wishlists");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Wishlists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Wishlists");

            migrationBuilder.AddColumn<int>(
                name: "ProductNameId",
                table: "Wishlists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_ProductNameId",
                table: "Wishlists",
                column: "ProductNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Products_ProductNameId",
                table: "Wishlists",
                column: "ProductNameId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
