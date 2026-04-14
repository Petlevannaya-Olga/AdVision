using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdVision.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTariffsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tariffs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    venue_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    price = table.Column<double>(type: "REAL", nullable: false),
                    end_date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    start_date = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tariffs", x => x.id);
                    table.CheckConstraint("CK_Tariff_DateInterval", "\"start_date\" <= \"end_date\"");
                    table.CheckConstraint("CK_Tariff_Price", "\"price\" >= 500");
                    table.ForeignKey(
                        name: "FK_tariffs_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tariffs_venue_id",
                table: "tariffs",
                column: "venue_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tariffs");
        }
    }
}
