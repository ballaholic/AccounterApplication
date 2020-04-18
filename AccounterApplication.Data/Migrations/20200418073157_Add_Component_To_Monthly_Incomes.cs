using Microsoft.EntityFrameworkCore.Migrations;

namespace AccounterApplication.Data.Migrations
{
    public partial class Add_Component_To_Monthly_Incomes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComponentId",
                table: "MonthlyIncomes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyIncomes_ComponentId",
                table: "MonthlyIncomes",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonthlyIncomes_Components_ComponentId",
                table: "MonthlyIncomes",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonthlyIncomes_Components_ComponentId",
                table: "MonthlyIncomes");

            migrationBuilder.DropIndex(
                name: "IX_MonthlyIncomes_ComponentId",
                table: "MonthlyIncomes");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "MonthlyIncomes");
        }
    }
}
