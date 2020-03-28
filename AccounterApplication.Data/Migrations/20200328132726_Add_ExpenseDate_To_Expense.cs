using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccounterApplication.Data.Migrations
{
    public partial class Add_ExpenseDate_To_Expense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseGroup_ExpenseGroupId",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseGroup",
                table: "ExpenseGroup");

            migrationBuilder.RenameTable(
                name: "ExpenseGroup",
                newName: "ExpenseGroups");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseGroup_IsDeleted",
                table: "ExpenseGroups",
                newName: "IX_ExpenseGroups_IsDeleted");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpenseDate",
                table: "Expenses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseGroups",
                table: "ExpenseGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseGroups_ExpenseGroupId",
                table: "Expenses",
                column: "ExpenseGroupId",
                principalTable: "ExpenseGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseGroups_ExpenseGroupId",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseGroups",
                table: "ExpenseGroups");

            migrationBuilder.DropColumn(
                name: "ExpenseDate",
                table: "Expenses");

            migrationBuilder.RenameTable(
                name: "ExpenseGroups",
                newName: "ExpenseGroup");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseGroups_IsDeleted",
                table: "ExpenseGroup",
                newName: "IX_ExpenseGroup_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseGroup",
                table: "ExpenseGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseGroup_ExpenseGroupId",
                table: "Expenses",
                column: "ExpenseGroupId",
                principalTable: "ExpenseGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
