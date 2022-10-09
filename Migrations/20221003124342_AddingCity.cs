using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdventurerOfficialProject.Migrations
{
    public partial class AddingCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ActivitiesDbSet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "ActivitiesDbSet");
        }
    }
}
