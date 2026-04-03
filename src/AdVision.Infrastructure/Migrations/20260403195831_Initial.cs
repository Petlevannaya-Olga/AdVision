using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdVision.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "venue_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_venue_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "venues",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    venue_type_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    region = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    district = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    city = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    street = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    house = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    latitude = table.Column<double>(type: "REAL", nullable: false),
                    longitude = table.Column<double>(type: "REAL", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    width = table.Column<double>(type: "REAL", nullable: false),
                    height = table.Column<double>(type: "REAL", nullable: false),
                    rating = table.Column<double>(type: "REAL", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_venues", x => x.id);
                    table.CheckConstraint("CK_Venue_City_NotEmpty", "length(\"city\") > 10");
                    table.CheckConstraint("CK_Venue_District_NotEmpty", "length(\"district\") > 10");
                    table.CheckConstraint("CK_Venue_Height", "\"height\" >= 100 AND \"height\" <= 10000");
                    table.CheckConstraint("CK_Venue_House_NotEmpty", "length(\"house\") > 0");
                    table.CheckConstraint("CK_Venue_Latitude", "\"latitude\" >= -90 AND \"latitude\" <= 90");
                    table.CheckConstraint("CK_Venue_Longitude", "\"longitude\" >= -180 AND \"longitude\" <= 180");
                    table.CheckConstraint("CK_Venue_Rating", "\"width\" >= 1 AND \"width\" <= 10");
                    table.CheckConstraint("CK_Venue_Region_NotEmpty", "length(\"region\") > 10");
                    table.CheckConstraint("CK_Venue_Street_NotEmpty", "length(\"street\") > 10");
                    table.CheckConstraint("CK_Venue_Width", "\"width\" >= 100 AND \"width\" <= 10000");
                    table.ForeignKey(
                        name: "FK_venues_venue_types_venue_type_id",
                        column: x => x.venue_type_id,
                        principalTable: "venue_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_venue_types_name",
                table: "venue_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_venues_venue_type_id",
                table: "venues",
                column: "venue_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "venues");

            migrationBuilder.DropTable(
                name: "venue_types");
        }
    }
}
