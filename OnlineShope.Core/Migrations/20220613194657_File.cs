using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShope.Core.Migrations
{
    public partial class File : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Thubmnail",
                table: "Product",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailFileExtension",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "ThumbnailFileName",
                table: "Product",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "ThumbnailFileSize",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thubmnail",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileExtension",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileName",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileSize",
                table: "Product");
        }
    }
}
