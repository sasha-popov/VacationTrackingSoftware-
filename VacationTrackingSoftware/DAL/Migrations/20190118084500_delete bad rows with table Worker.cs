using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class deletebadrowswithtableWorker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Workers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Workers",
                nullable: true);
        }
    }
}
