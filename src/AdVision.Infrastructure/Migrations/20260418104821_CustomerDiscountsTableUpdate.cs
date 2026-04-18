using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdVision.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CustomerDiscountsTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_discounts_customers_customer_id",
                table: "customer_discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_customer_discounts_discounts_discount_id",
                table: "customer_discounts");

            migrationBuilder.AddForeignKey(
                name: "FK_customer_discounts_customers_customer_id",
                table: "customer_discounts",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_customer_discounts_discounts_discount_id",
                table: "customer_discounts",
                column: "discount_id",
                principalTable: "discounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_discounts_customers_customer_id",
                table: "customer_discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_customer_discounts_discounts_discount_id",
                table: "customer_discounts");

            migrationBuilder.AddForeignKey(
                name: "FK_customer_discounts_customers_customer_id",
                table: "customer_discounts",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_customer_discounts_discounts_discount_id",
                table: "customer_discounts",
                column: "discount_id",
                principalTable: "discounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
