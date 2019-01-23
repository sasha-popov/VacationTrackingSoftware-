using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class addteamuserinteamuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TeamUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamUsers_UserId",
                table: "TeamUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_AspNetUsers_UserId",
                table: "TeamUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_AspNetUsers_UserId",
                table: "TeamUsers");

            migrationBuilder.DropIndex(
                name: "IX_TeamUsers_UserId",
                table: "TeamUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TeamUsers");
        }
    }
}
