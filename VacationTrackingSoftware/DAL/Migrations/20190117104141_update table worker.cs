using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class updatetableworker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Worker_ManagerId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_Worker_UserId",
                table: "TeamUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVacantionRequests_Worker_UserId",
                table: "UserVacantionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Worker_AspNetUsers_UserIdId",
                table: "Worker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Worker",
                table: "Worker");

            migrationBuilder.RenameTable(
                name: "Worker",
                newName: "Workers");

            migrationBuilder.RenameColumn(
                name: "UserIdId",
                table: "Workers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Worker_UserIdId",
                table: "Workers",
                newName: "IX_Workers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workers",
                table: "Workers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Workers_ManagerId",
                table: "Teams",
                column: "ManagerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Workers_UserId",
                table: "TeamUsers",
                column: "UserId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Workers_ManagerId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_Workers_UserId",
                table: "TeamUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVacantionRequests_Workers_UserId",
                table: "UserVacantionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_AspNetUsers_UserId",
                table: "Workers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workers",
                table: "Workers");

            migrationBuilder.RenameTable(
                name: "Workers",
                newName: "Worker");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Worker",
                newName: "UserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Workers_UserId",
                table: "Worker",
                newName: "IX_Worker_UserIdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Worker",
                table: "Worker",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Worker_ManagerId",
                table: "Teams",
                column: "ManagerId",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Worker_UserId",
                table: "TeamUsers",
                column: "UserId",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVacantionRequests_Worker_UserId",
                table: "UserVacantionRequests",
                column: "UserId",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_AspNetUsers_UserIdId",
                table: "Worker",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
