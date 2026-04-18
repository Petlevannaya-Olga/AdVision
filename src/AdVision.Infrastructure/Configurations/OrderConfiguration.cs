using AdVision.Domain;
using AdVision.Domain.Contracts;
using AdVision.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdVision.Infrastructure.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new OrderId(value)
            );

        builder
            .Property(x => x.ContractId)
            .HasColumnName("contract_id")
            .HasConversion(
                id => id.Value,
                value => new ContractId(value)
            )
            .IsRequired();

        builder
            .Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder
            .Property(x => x.TotalAmount)
            .HasColumnName("total_amount")
            .HasConversion(
                money => money.Value,
                value => Money.Create(value).Value
            )
            .HasPrecision(18, 2)
            .IsRequired();

        builder
            .HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Items)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasIndex(x => x.ContractId);

        builder
            .HasIndex(x => x.Status);

        builder
            .HasIndex(x => new { x.ContractId, x.Status });

        builder
            .HasOne<Contract>()
            .WithMany()
            .HasForeignKey(x => x.ContractId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}