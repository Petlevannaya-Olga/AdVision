using AdVision.Domain.CustomerDiscounts;
using AdVision.Domain.Customers;
using AdVision.Domain.Discounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdVision.Infrastructure.Configurations;

public sealed class CustomerDiscountConfiguration : IEntityTypeConfiguration<CustomerDiscount>
{
    public void Configure(EntityTypeBuilder<CustomerDiscount> builder)
    {
        builder.ToTable("customer_discounts");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new CustomerDiscountId(value)
            );

        builder
            .Property(x => x.CustomerId)
            .HasColumnName("customer_id")
            .HasConversion(
                id => id.Value,
                value => new CustomerId(value)
            )
            .IsRequired();

        builder
            .Property(x => x.DiscountId)
            .HasColumnName("discount_id")
            .HasConversion(
                id => id.Value,
                value => new DiscountId(value)
            )
            .IsRequired();

        builder
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<Discount>()
            .WithMany()
            .HasForeignKey(x => x.DiscountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => new { x.CustomerId, x.DiscountId })
            .IsUnique();
    }
}