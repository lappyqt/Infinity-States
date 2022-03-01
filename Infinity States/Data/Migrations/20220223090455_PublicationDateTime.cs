using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinity_States.Migrations
{
    public partial class PublicationDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationDateTime",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicationDateTime",
                table: "Articles");
        }
    }
}
