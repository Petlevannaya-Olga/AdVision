using AdVision.Domain;
using AdVision.Domain.Orders;
using AdVision.Domain.Tariffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdVision.Infrastructure.Configurations;

public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new OrderItemId(value)
            );

        builder
            .Property(x => x.OrderId)
            .HasColumnName("order_id")
            .HasConversion(
                id => id.Value,
                value => new OrderId(value)
            )
            .IsRequired();

        builder
            .Property(x => x.TariffId)
            .HasColumnName("tariff_id")
            .HasConversion(
                id => id.Value,
                value => new TariffId(value)
            )
            .IsRequired();

        builder
            .Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder
            .Property(x => x.Price)
            .HasColumnName("price")
            .HasConversion(
                money => money.Value,
                value => Money.Create(value).Value
            )
            .HasPrecision(18, 2)
            .IsRequired();

        builder.OwnsOne(x => x.Period, periodBuilder =>
        {
            periodBuilder
                .Property(x => x.StartDate)
                .HasColumnName("start_date")
                .IsRequired();

            periodBuilder
                .Property(x => x.EndDate)
                .HasColumnName("end_date")
                .IsRequired();
        });

        builder
            .HasIndex(x => x.OrderId);

        builder
            .HasIndex(x => x.TariffId);

        builder
            .HasIndex(x => x.Status);

        builder
            .HasIndex(x => new { x.OrderId, x.TariffId });

        builder
            .HasIndex(x => new { x.OrderId, x.Status });

        builder
            .HasOne<Order>()
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<Tariff>()
            .WithMany()
            .HasForeignKey(x => x.TariffId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}