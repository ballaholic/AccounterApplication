using Microsoft.EntityFrameworkCore.Migrations;

namespace AccounterApplication.Data.Migrations
{
    public partial class Added_Code_To_Currencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Currencies",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Currencies");
        }
    }
}
