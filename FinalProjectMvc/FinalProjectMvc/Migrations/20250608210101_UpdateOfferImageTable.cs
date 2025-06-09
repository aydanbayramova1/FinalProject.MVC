using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectMvc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOfferImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CssClass",
                table: "OfferImages",
                newName: "ImageType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageType",
                table: "OfferImages",
                newName: "CssClass");
        }
    }
}
