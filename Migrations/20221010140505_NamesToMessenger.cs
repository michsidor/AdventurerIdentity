using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdventurerOfficialProject.Migrations
{
    public partial class NamesToMessenger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecipientName",
                table: "MessageDbSet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "MessageDbSet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipientName",
                table: "MessageDbSet");

            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "MessageDbSet");
        }
    }
}
