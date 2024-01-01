using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class Bids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HouseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Bidder = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "Amount", "Bidder", "HouseId" },
                values: new object[,]
                {
                    { 1, 200000, "Sonia Reading", 1 },
                    { 2, 202400, "Dick Johnson", 1 },
                    { 3, 302400, "Mohammed Vahls", 2 },
                    { 4, 310500, "Jane Williams", 2 },
                    { 5, 315400, "John Kepler", 2 },
                    { 6, 201000, "Bill Mentor", 3 },
                    { 7, 410000, "Melissa Kirk", 4 },
                    { 8, 450000, "Scott Max", 4 },
                    { 9, 470000, "Christine James", 4 },
                    { 10, 450000, "Omesh Carim", 5 },
                    { 11, 150000, "Charlotte Max", 5 },
                    { 12, 170000, "Marcus Scott", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_HouseId",
                table: "Bids",
                column: "HouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bids");
        }
    }
}
