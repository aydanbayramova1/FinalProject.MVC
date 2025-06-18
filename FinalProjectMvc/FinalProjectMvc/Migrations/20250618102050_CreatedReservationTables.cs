using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectMvc.Migrations
{
    /// <inheritdoc />
    public partial class CreatedReservationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_MenuProducts_MenuProductId",
                table: "ProductSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductSizes_MenuProductId",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "MenuProductId",
                table: "ProductSizes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuProductId",
                table: "ProductSizes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_MenuProductId",
                table: "ProductSizes",
                column: "MenuProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_MenuProducts_MenuProductId",
                table: "ProductSizes",
                column: "MenuProductId",
                principalTable: "MenuProducts",
                principalColumn: "Id");
        }
    }
}
