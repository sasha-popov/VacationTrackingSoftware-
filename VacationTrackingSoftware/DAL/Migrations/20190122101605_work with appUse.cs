using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class workwithappUse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVacantionRequests_Workers_UserId",
                table: "UserVacantionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_AspNetUsers_UserId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_UserId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Workers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserVacantionRequests",
                newName: "WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_UserVacantionRequests_UserId",
                table: "UserVacantionRequests",
                newName: "IX_UserVacantionRequests_WorkerId");

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WorkerId",
                table: "AspNetUsers",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Workers_WorkerId",
                table: "AspNetUsers",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVacantionRequests_Workers_WorkerId",
                table: "UserVacantionRequests",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Workers_WorkerId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVacantionRequests_Workers_WorkerId",
                table: "UserVacantionRequests");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WorkerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "UserVacantionRequests",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserVacantionRequests_WorkerId",
                table: "UserVacantionRequests",
                newName: "IX_UserVacantionRequests_UserId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Workers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_UserId",
                table: "Workers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVacantionRequests_Workers_UserId",
                table: "UserVacantionRequests",
                column: "UserId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_AspNetUsers_UserId",
                table: "Workers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
