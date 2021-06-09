using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTest.Migrations
{
    public partial class updating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role_id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isLock",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role_id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "isLock",
                table: "Users");
        }
    }
}
