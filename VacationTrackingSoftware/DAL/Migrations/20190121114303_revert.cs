using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class revert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_Workers_UserId",
                table: "TeamUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TeamUsers",
                newName: "WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamUsers_UserId",
                table: "TeamUsers",
                newName: "IX_TeamUsers_WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Workers_WorkerId",
                table: "TeamUsers",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_Workers_WorkerId",
                table: "TeamUsers");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "TeamUsers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamUsers_WorkerId",
                table: "TeamUsers",
                newName: "IX_TeamUsers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Workers_UserId",
                table: "TeamUsers",
                column: "UserId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
