using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdVision.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DiscountsRemoveConstraints : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_discounts_min_total",
                table: "discounts",
                sql: "\"min_total\" > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_discounts_percent",
                table: "discounts",
                sql: "\"percent\" >= 1 AND \"percent\" <= 100");
        }
    }
}
