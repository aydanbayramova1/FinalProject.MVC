using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectMvc.Migrations
{
    /// <inheritdoc />
    public partial class CreateMenuProductTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuProductId",
                table: "ProductSizes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MenuProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuProducts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuProducts_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MenuProductSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuProductId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuProductSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuProductSizes_MenuProducts_MenuProductId",
                        column: x => x.MenuProductId,
                        principalTable: "MenuProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuProductSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_MenuProductId",
                table: "ProductSizes",
                column: "MenuProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuProducts_CategoryId",
                table: "MenuProducts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuProducts_SizeId",
                table: "MenuProducts",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuProductSizes_MenuProductId",
                table: "MenuProductSizes",
                column: "MenuProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuProductSizes_SizeId",
                table: "MenuProductSizes",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_MenuProducts_MenuProductId",
                table: "ProductSizes",
                column: "MenuProductId",
                principalTable: "MenuProducts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_MenuProducts_MenuProductId",
                table: "ProductSizes");

            migrationBuilder.DropTable(
                name: "MenuProductSizes");

            migrationBuilder.DropTable(
                name: "MenuProducts");

            migrationBuilder.DropIndex(
                name: "IX_ProductSizes_MenuProductId",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "MenuProductId",
                table: "ProductSizes");
        }
    }
}
