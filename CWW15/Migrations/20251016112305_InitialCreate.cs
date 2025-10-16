using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CWW15.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Digital Goods" },
                    { 2, "Home Appliances" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "Color", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "Apple", 1, "Silver", "Laptop", 1200.00m, 15 },
                    { 2, "Samsung", 1, "Black", "Smartphone", 800.00m, 50 },
                    { 3, "Sony", 1, "White", "Headphones", 150.50m, 100 },
                    { 4, "Garmin", 1, "Gray", "Smartwatch", 250.00m, 30 },
                    { 5, "Microsoft", 1, "Black", "Gaming Console", 500.00m, 25 },
                    { 6, "LG", 2, "Stainless Steel", "Refrigerator", 1500.00m, 10 },
                    { 7, "Bosch", 2, "White", "Washing Machine", 900.00m, 20 },
                    { 8, "Panasonic", 2, "Black", "Microwave Oven", 200.00m, 40 },
                    { 9, "Dyson", 2, "Red", "Vacuum Cleaner", 300.00m, 35 },
                    { 10, "Philips", 2, "Black", "Coffee Maker", 120.99m, 60 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
