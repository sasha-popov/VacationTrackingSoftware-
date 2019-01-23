using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class adduserinteam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ManagerId",
                table: "Teams",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_ManagerId",
                table: "Teams",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_ManagerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ManagerId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Teams");
        }
    }
}
