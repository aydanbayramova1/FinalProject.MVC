using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectMvc.Migrations
{
    /// <inheritdoc />
    public partial class CreateAboutRestaurantTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutRestaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QualityMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AtmosphereNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadBaristaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimingTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimingSubtitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimingDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeekdayTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeekendTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HappyHour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HolidayStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HolidayNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutRestaurants", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutRestaurants");
        }
    }
}
