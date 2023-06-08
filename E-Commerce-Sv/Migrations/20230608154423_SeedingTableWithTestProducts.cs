using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce_Sv.Migrations
{
    /// <inheritdoc />
    public partial class SeedingTableWithTestProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CategoryId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Desktop", 1, "graficka komponenta", "AMD Radeon RX 580", 150 },
                    { 2, "Desktop", 1, "graficka komponenta", "NVIDIA GEFORCE GTX", 250 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
