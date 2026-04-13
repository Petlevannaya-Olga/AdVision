using AdVision.Domain.VenueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdVision.Infrastructure.Configurations;

public sealed class VenueTypeConfiguration : IEntityTypeConfiguration<VenueType>
{
    public void Configure(EntityTypeBuilder<VenueType> builder)
    {
        builder.ToTable("venue_types");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new VenueTypeId(value)
            );

        builder
            .Property(x => x.Name)
            .HasConversion(
                v => v.Value,
                v => VenueTypeName.Create(v).Value
            )
            .HasColumnName("name")
            .HasMaxLength(VenueTypeName.MAX_LENGTH)
            .IsRequired();
        
        builder
            .HasIndex(x => x.Name)
            .IsUnique();
    }
}