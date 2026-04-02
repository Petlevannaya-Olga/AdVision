using AdVision.Domain.VenueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;

namespace AdVision.Infrastructure.Configurations;

public class VenueTypeConfiguration : IEntityTypeConfiguration<VenueType>
{
    public void Configure(EntityTypeBuilder<VenueType> builder)
    {
        builder.ToTable("venue_types");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(x => x.Value, name => new VenueTypeId(name));

        builder
            .Property(x => x.Name)
            .HasConversion(
                v => v.Value,
                v => VenueName.Create(v).Value
            )
            .HasColumnName("name")
            .HasMaxLength(LengthConstants.LENGTH_500)
            .IsRequired();
    }
}