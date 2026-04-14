using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdVision.Infrastructure.Migrations
{
    public partial class AddTariffsTableUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                     CREATE UNIQUE INDEX IF NOT EXISTS "IX_tariffs_venue_id_start_date_end_date"
                                     ON "tariffs" ("venue_id", "start_date", "end_date");
                                 """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                     DROP INDEX IF EXISTS "IX_tariffs_venue_id_start_date_end_date";
                                 """);
        }
    }
}
