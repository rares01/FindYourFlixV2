using Microsoft.EntityFrameworkCore.Migrations;

namespace FindYourFlix.Migrations
{
    public partial class AddIndexForUserAndMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Movies");
        }
    }
}
