using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Factory.Migrations
{
    /// <inheritdoc />
    public partial class AddBatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "batchId",
                table: "Materials",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Productid = table.Column<int>(type: "integer", nullable: true),
                    MaterialId = table.Column<int>(type: "integer", nullable: false),
                    IsRun = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Batches_Products_Productid",
                        column: x => x.Productid,
                        principalTable: "Products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_batchId",
                table: "Materials",
                column: "batchId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_Productid",
                table: "Batches",
                column: "Productid");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Batches_batchId",
                table: "Materials",
                column: "batchId",
                principalTable: "Batches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Batches_batchId",
                table: "Materials");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropIndex(
                name: "IX_Materials_batchId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "batchId",
                table: "Materials");
        }
    }
}
