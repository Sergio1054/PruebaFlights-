using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flights_Serve.Migrations
{
    /// <inheritdoc />
    public partial class Flights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Origin = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Destination = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal (18,2)", nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FlightCarrier = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FlightNumber = table.Column<decimal>(type: "decimal (18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Origin",
                table: "Flights",
                column: "Origin",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
