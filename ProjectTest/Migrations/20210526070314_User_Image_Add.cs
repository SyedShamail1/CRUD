using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTest.Migrations
{
    public partial class User_Image_Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User_Image",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_Image",
                table: "Users");
        }
    }
}
