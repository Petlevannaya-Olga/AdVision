using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdVision.Infrastructure.Migrations
{
    public partial class VenueNameUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                     CREATE UNIQUE INDEX IF NOT EXISTS "IX_venues_name"
                                     ON "venues" ("name");
                                 """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                     DROP INDEX IF EXISTS "IX_venues_name";
                                 """);
        }
    }
}
