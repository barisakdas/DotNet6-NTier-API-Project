using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFeatures",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeatures", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductFeatures_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2022, 3, 10, 22, 30, 42, 85, DateTimeKind.Local).AddTicks(2576), "Kalemler", null });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[] { 2, new DateTime(2022, 3, 10, 22, 30, 42, 85, DateTimeKind.Local).AddTicks(2601), "Kitaplar", null });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[] { 3, new DateTime(2022, 3, 10, 22, 30, 42, 85, DateTimeKind.Local).AddTicks(2603), "Defterler", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "CategoryID", "CreatedDate", "Name", "Price", "Stock", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 3, 10, 22, 30, 42, 85, DateTimeKind.Local).AddTicks(2772), "Rotring", 50m, 100, null },
                    { 2, 1, new DateTime(2022, 3, 10, 22, 30, 42, 85, DateTimeKind.Local).AddTicks(2850), "Faber", 10m, 50, null },
                    { 3, 2, new DateTime(2022, 3, 10, 22, 30, 42, 85, DateTimeKind.Local).AddTicks(2853), "Sefiller", 10m, 50, null },
                    { 4, 2, new DateTime(2022, 3, 10, 22, 30, 42, 85, DateTimeKind.Local).AddTicks(2855), "Faber", 10m, 50, null },
                    { 5, 3, new DateTime(2022, 3, 10, 22, 30, 42, 85, DateTimeKind.Local).AddTicks(2856), "Mopak", 12m, 152, null },
                    { 6, 3, new DateTime(2022, 3, 10, 22, 30, 42, 85, DateTimeKind.Local).AddTicks(2861), "Other", 18m, 250, null }
                });

            migrationBuilder.InsertData(
                table: "ProductFeatures",
                columns: new[] { "ID", "Color", "ProductID" },
                values: new object[] { 1, "Kırmızı", 1 });

            migrationBuilder.InsertData(
                table: "ProductFeatures",
                columns: new[] { "ID", "Color", "ProductID" },
                values: new object[] { 2, "Mavi", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatures_ProductID",
                table: "ProductFeatures",
                column: "ProductID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductFeatures");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
