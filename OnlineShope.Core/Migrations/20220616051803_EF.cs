using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShope.Core.Migrations
{
    public partial class EF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceWithComma",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ThumbnailFileExtension",
                table: "Product",
                newName: "ThumbnailFileExtenstion");

            migrationBuilder.RenameColumn(
                name: "Thubmnail",
                table: "Product",
                newName: "Thumbnail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThumbnailFileExtenstion",
                table: "Product",
                newName: "ThumbnailFileExtension");

            migrationBuilder.RenameColumn(
                name: "Thumbnail",
                table: "Product",
                newName: "Thubmnail");

            migrationBuilder.AddColumn<long>(
                name: "PriceWithComma",
                table: "Product",
                type: "bigint",
                nullable: true);
        }
    }
}
