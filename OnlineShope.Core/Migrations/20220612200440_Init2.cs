using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShope.Core.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermisionId",
                table: "RolePermision");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PermisionId",
                table: "RolePermision",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
