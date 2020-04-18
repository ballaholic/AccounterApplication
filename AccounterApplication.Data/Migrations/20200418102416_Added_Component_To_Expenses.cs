using Microsoft.EntityFrameworkCore.Migrations;

namespace AccounterApplication.Data.Migrations
{
    public partial class Added_Component_To_Expenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComponentId",
                table: "Expenses",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ComponentId",
                table: "Expenses",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Components_ComponentId",
                table: "Expenses",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Components_ComponentId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_ComponentId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "Expenses");
        }
    }
}
