using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectMvc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContactMessageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReplied",
                table: "ContactMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Reply",
                table: "ContactMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReplyDate",
                table: "ContactMessages",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReplied",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "Reply",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "ReplyDate",
                table: "ContactMessages");
        }
    }
}
