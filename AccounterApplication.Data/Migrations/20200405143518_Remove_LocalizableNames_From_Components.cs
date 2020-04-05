using Microsoft.EntityFrameworkCore.Migrations;

namespace AccounterApplication.Data.Migrations
{
    public partial class Remove_LocalizableNames_From_Components : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameBG",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "NameEN",
                table: "Components");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Components",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Components");

            migrationBuilder.AddColumn<string>(
                name: "NameBG",
                table: "Components",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameEN",
                table: "Components",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
