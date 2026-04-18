using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdVision.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TariffsTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tariffs_venues_venue_id",
                table: "tariffs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Tariff_DateInterval",
                table: "tariffs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Tariff_Price",
                table: "tariffs");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "tariffs",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<decimal>(
                name: "percent",
                table: "discounts",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.CreateIndex(
                name: "IX_tariffs_end_date",
                table: "tariffs",
                column: "end_date");

            migrationBuilder.CreateIndex(
                name: "IX_tariffs_price",
                table: "tariffs",
                column: "price");

            migrationBuilder.CreateIndex(
                name: "IX_tariffs_start_date",
                table: "tariffs",
                column: "start_date");

            migrationBuilder.CreateIndex(
                name: "IX_tariffs_start_date_end_date",
                table: "tariffs",
                columns: new[] { "start_date", "end_date" });

            migrationBuilder.CreateIndex(
                name: "IX_tariffs_venue_id_price",
                table: "tariffs",
                columns: new[] { "venue_id", "price" });

            migrationBuilder.AddForeignKey(
                name: "FK_tariffs_venues_venue_id",
                table: "tariffs",
                column: "venue_id",
                principalTable: "venues",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tariffs_venues_venue_id",
                table: "tariffs");

            migrationBuilder.DropIndex(
                name: "IX_tariffs_end_date",
                table: "tariffs");

            migrationBuilder.DropIndex(
                name: "IX_tariffs_price",
                table: "tariffs");

            migrationBuilder.DropIndex(
                name: "IX_tariffs_start_date",
                table: "tariffs");

            migrationBuilder.DropIndex(
                name: "IX_tariffs_start_date_end_date",
                table: "tariffs");

            migrationBuilder.DropIndex(
                name: "IX_tariffs_venue_id_price",
                table: "tariffs");

            migrationBuilder.AlterColumn<double>(
                name: "price",
                table: "tariffs",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<double>(
                name: "percent",
                table: "discounts",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Tariff_DateInterval",
                table: "tariffs",
                sql: "\"start_date\" <= \"end_date\"");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Tariff_Price",
                table: "tariffs",
                sql: "\"price\" >= 500");

            migrationBuilder.AddForeignKey(
                name: "FK_tariffs_venues_venue_id",
                table: "tariffs",
                column: "venue_id",
                principalTable: "venues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
