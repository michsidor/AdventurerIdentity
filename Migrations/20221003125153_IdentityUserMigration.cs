using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdventurerOfficialProject.Migrations
{
    public partial class IdentityUserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "identityUserId",
                table: "ActivitiesDbSet",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesDbSet_identityUserId",
                table: "ActivitiesDbSet",
                column: "identityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivitiesDbSet_AspNetUsers_identityUserId",
                table: "ActivitiesDbSet",
                column: "identityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivitiesDbSet_AspNetUsers_identityUserId",
                table: "ActivitiesDbSet");

            migrationBuilder.DropIndex(
                name: "IX_ActivitiesDbSet_identityUserId",
                table: "ActivitiesDbSet");

            migrationBuilder.DropColumn(
                name: "identityUserId",
                table: "ActivitiesDbSet");
        }
    }
}
