using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdVision.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DiscountsTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_discounts_min_total",
                table: "discounts");

            migrationBuilder.DropCheckConstraint(
                name: "CK_discounts_percent",
                table: "discounts");

            migrationBuilder.AlterColumn<decimal>(
                name: "min_total",
                table: "discounts",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_discounts_min_total",
                table: "discounts",
                sql: "\"min_total\" > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_discounts_percent",
                table: "discounts",
                sql: "\"percent\" >= 1 AND \"percent\" <= 100");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_discounts_min_total",
                table: "discounts");

            migrationBuilder.DropCheckConstraint(
                name: "CK_discounts_percent",
                table: "discounts");

            migrationBuilder.AlterColumn<decimal>(
                name: "min_total",
                table: "discounts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddCheckConstraint(
                name: "CK_discounts_min_total",
                table: "discounts",
                sql: "min_total > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_discounts_percent",
                table: "discounts",
                sql: "percent >= 1 AND percent <= 100");
        }
    }
}
