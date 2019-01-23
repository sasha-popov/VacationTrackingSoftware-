using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class createrowHrUserintablevacationPolicies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HrUserId",
                table: "VacationPolicies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VacationPolicies_HrUserId",
                table: "VacationPolicies",
                column: "HrUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationPolicies_AspNetUsers_HrUserId",
                table: "VacationPolicies",
                column: "HrUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationPolicies_AspNetUsers_HrUserId",
                table: "VacationPolicies");

            migrationBuilder.DropIndex(
                name: "IX_VacationPolicies_HrUserId",
                table: "VacationPolicies");

            migrationBuilder.DropColumn(
                name: "HrUserId",
                table: "VacationPolicies");
        }
    }
}
