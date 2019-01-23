using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class teamuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Workers_ManagerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ManagerId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Teams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ManagerId",
                table: "Teams",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Workers_ManagerId",
                table: "Teams",
                column: "ManagerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
