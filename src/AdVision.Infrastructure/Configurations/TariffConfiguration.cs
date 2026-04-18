using AdVision.Domain;
using AdVision.Domain.Tariffs;
using AdVision.Domain.Venues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdVision.Infrastructure.Configurations;

public sealed class TariffConfiguration : IEntityTypeConfiguration<Tariff>
{
    public void Configure(EntityTypeBuilder<Tariff> builder)
    {
        builder.ToTable("tariffs");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new TariffId(value)
            );

        builder
            .Property(x => x.VenueId)
            .HasColumnName("venue_id")
            .HasConversion(
                id => id.Value,
                value => new VenueId(value)
            )
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

        builder.OwnsOne(x => x.Interval, intervalBuilder =>
        {
            intervalBuilder
                .Property(x => x.StartDate)
                .HasColumnName("start_date")
                .IsRequired();

            intervalBuilder
                .Property(x => x.EndDate)
                .HasColumnName("end_date")
                .IsRequired();

            intervalBuilder.HasIndex(x => x.StartDate);
            intervalBuilder.HasIndex(x => x.EndDate);
            intervalBuilder.HasIndex(x => new { x.StartDate, x.EndDate });
        });

        builder
            .HasIndex(x => x.VenueId);

        builder
            .HasIndex(x => x.Price);

        builder
            .HasIndex(x => new { x.VenueId, x.Price });

        builder
            .HasOne(x => x.Venue)
            .WithMany()
            .HasForeignKey(x => x.VenueId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}