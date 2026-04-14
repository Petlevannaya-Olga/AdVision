using AdVision.Domain.Tariffs;
using AdVision.Domain.Venues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdVision.Infrastructure.Configurations;

public sealed class TariffConfiguration : IEntityTypeConfiguration<Tariff>
{
    public void Configure(EntityTypeBuilder<Tariff> builder)
    {
        builder.ToTable("tariffs", table =>
        {
            table.HasCheckConstraint(
                "CK_Tariff_Price",
                "\"price\" >= 500");

            table.HasCheckConstraint(
                "CK_Tariff_DateInterval",
                "\"start_date\" <= \"end_date\"");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new TariffId(value));

        builder.Property(x => x.VenueId)
            .HasColumnName("venue_id")
            .HasConversion(
                id => id.Value,
                value => new VenueId(value))
            .IsRequired();

        builder.ComplexProperty(x => x.Interval, intervalBuilder =>
        {
            intervalBuilder.Property(x => x.StartDate)
                .HasColumnName("start_date")
                .IsRequired();

            intervalBuilder.Property(x => x.EndDate)
                .HasColumnName("end_date")
                .IsRequired();
        });

        builder.Property(x => x.Price)
            .HasColumnName("price")
            .IsRequired();

        builder.HasIndex(x => x.VenueId);

        // builder.HasIndex("venue_id", "start_date", "end_date")
        //     .IsUnique();

        builder.HasOne(x => x.Venue)
            .WithMany()
            .HasForeignKey(x => x.VenueId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}