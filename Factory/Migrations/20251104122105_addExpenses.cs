using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Factory.Migrations
{
    /// <inheritdoc />
    public partial class addExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sellers_Account_Accountid",
                table: "sellers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sellers",
                table: "sellers");

            migrationBuilder.RenameTable(
                name: "sellers",
                newName: "Sellers");

            migrationBuilder.RenameIndex(
                name: "IX_sellers_Accountid",
                table: "Sellers",
                newName: "IX_Sellers_Accountid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sellers",
                table: "Sellers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Details = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    dateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_Account_Accountid",
                table: "Sellers",
                column: "Accountid",
                principalTable: "Account",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_Account_Accountid",
                table: "Sellers");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sellers",
                table: "Sellers");

            migrationBuilder.RenameTable(
                name: "Sellers",
                newName: "sellers");

            migrationBuilder.RenameIndex(
                name: "IX_Sellers_Accountid",
                table: "sellers",
                newName: "IX_sellers_Accountid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sellers",
                table: "sellers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sellers_Account_Accountid",
                table: "sellers",
                column: "Accountid",
                principalTable: "Account",
                principalColumn: "id");
        }
    }
}
