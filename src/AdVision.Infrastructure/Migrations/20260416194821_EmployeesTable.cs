using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdVision.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmployeesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    middle_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    passport_series = table.Column<string>(type: "TEXT", maxLength: 4, nullable: false),
                    passport_number = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    phone_number = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    position_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.id);
                    table.ForeignKey(
                        name: "FK_employees_positions_position_id",
                        column: x => x.position_id,
                        principalTable: "positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_passport_series_passport_number",
                table: "employees",
                columns: new[] { "passport_series", "passport_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_phone_number",
                table: "employees",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_position_id",
                table: "employees",
                column: "position_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");
        }
    }
}
