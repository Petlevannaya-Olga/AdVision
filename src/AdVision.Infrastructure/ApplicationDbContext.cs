using AdVision.Domain.Discounts;
using AdVision.Domain.Employees;
using AdVision.Domain.Positions;
using AdVision.Domain.Tariffs;
using AdVision.Domain.Venues;
using AdVision.Domain.VenueTypes;
using Microsoft.EntityFrameworkCore;

namespace AdVision.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<VenueType> VenueTypes { get; set; }

    public DbSet<Venue> Venues { get; set; }

    public DbSet<Tariff> Tariffs { get; set; }

    public DbSet<Position> Positions { get; set; }

    public DbSet<Discount> Discounts { get; set; }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}