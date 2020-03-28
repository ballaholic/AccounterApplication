using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccounterApplication.Data.Migrations
{
    public partial class Add_ExpenseGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenseGroupId",
                table: "Expenses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExpenseGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    NameBG = table.Column<string>(nullable: true),
                    IsMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseGroupId",
                table: "Expenses",
                column: "ExpenseGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseGroup_IsDeleted",
                table: "ExpenseGroup",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseGroup_ExpenseGroupId",
                table: "Expenses",
                column: "ExpenseGroupId",
                principalTable: "ExpenseGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseGroup_ExpenseGroupId",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "ExpenseGroup");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_ExpenseGroupId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "ExpenseGroupId",
                table: "Expenses");
        }
    }
}
