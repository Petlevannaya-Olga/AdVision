using AdVision.Domain.Discounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdVision.Infrastructure.Configurations;

public sealed class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("discounts", t =>
        {
            t.HasCheckConstraint("CK_discounts_percent", "percent >= 1 AND percent <= 100");
            t.HasCheckConstraint("CK_discounts_min_total", "min_total > 0");
        });

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new DiscountId(value)
            );

        builder
            .Property(x => x.Name)
            .HasConversion(
                v => v.Value,
                v => DiscountName.Create(v).Value
            )
            .HasColumnName("name")
            .HasMaxLength(DiscountName.MAX_LENGTH)
            .IsRequired();

        builder
            .Property(x => x.Percent)
            .HasConversion(
                v => v.Value,
                v => DiscountPercent.Create(v).Value
            )
            .HasColumnName("percent")
            .IsRequired();

        builder
            .Property(x => x.MinTotal)
            .HasConversion(
                v => v.Value,
                v => DiscountMinTotal.Create(v).Value
            )
            .HasColumnName("min_total")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder
            .HasIndex(x => x.Name)
            .IsUnique();
    }
}