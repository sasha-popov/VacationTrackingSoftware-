using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class updataapp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVacantionRequests_Workers_WorkerId",
                table: "UserVacantionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationPolicies_AspNetUsers_HrUserId",
                table: "VacationPolicies");

            migrationBuilder.DropIndex(
                name: "IX_VacationPolicies_HrUserId",
                table: "VacationPolicies");

            migrationBuilder.DropIndex(
                name: "IX_UserVacantionRequests_WorkerId",
                table: "UserVacantionRequests");

            migrationBuilder.DropColumn(
                name: "HrUserId",
                table: "VacationPolicies");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "UserVacantionRequests");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserVacantionRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserVacantionRequests_UserId",
                table: "UserVacantionRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVacantionRequests_AspNetUsers_UserId",
                table: "UserVacantionRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVacantionRequests_AspNetUsers_UserId",
                table: "UserVacantionRequests");

            migrationBuilder.DropIndex(
                name: "IX_UserVacantionRequests_UserId",
                table: "UserVacantionRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserVacantionRequests");

            migrationBuilder.AddColumn<string>(
                name: "HrUserId",
                table: "VacationPolicies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "UserVacantionRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VacationPolicies_HrUserId",
                table: "VacationPolicies",
                column: "HrUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVacantionRequests_WorkerId",
                table: "UserVacantionRequests",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVacantionRequests_Workers_WorkerId",
                table: "UserVacantionRequests",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VacationPolicies_AspNetUsers_HrUserId",
                table: "VacationPolicies",
                column: "HrUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
