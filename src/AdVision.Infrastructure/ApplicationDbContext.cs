using AdVision.Domain.Contracts;
using AdVision.Domain.CustomerDiscounts;
using AdVision.Domain.Customers;
using AdVision.Domain.Discounts;
using AdVision.Domain.Employees;
using AdVision.Domain.Orders;
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
    
    public DbSet<Customer> Customers { get; set; }
    
    public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }
    
    public DbSet<Contract> Contracts { get; set; }
    
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}