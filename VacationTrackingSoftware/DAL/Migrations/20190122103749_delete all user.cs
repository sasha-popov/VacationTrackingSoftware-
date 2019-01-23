using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class deletealluser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_Workers_UserId",
                table: "TeamUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationPolicies_AspNetUsers_HrUserId",
                table: "VacationPolicies");

            migrationBuilder.DropIndex(
                name: "IX_VacationPolicies_HrUserId",
                table: "VacationPolicies");

            migrationBuilder.DropIndex(
                name: "IX_TeamUsers_UserId",
                table: "TeamUsers");

            migrationBuilder.DropColumn(
                name: "HrUserId",
                table: "VacationPolicies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TeamUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HrUserId",
                table: "VacationPolicies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TeamUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VacationPolicies_HrUserId",
                table: "VacationPolicies",
                column: "HrUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamUsers_UserId",
                table: "TeamUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Workers_UserId",
                table: "TeamUsers",
                column: "UserId",
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
