using AdVision.Domain.Positions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdVision.Infrastructure.Configurations;

public sealed class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new PositionId(value)
            );

        builder
            .Property(x => x.Name)
            .HasConversion(
                v => v.Value,
                v => PositionName.Create(v).Value
            )
            .HasColumnName("name")
            .HasMaxLength(PositionName.MAX_LENGTH)
            .IsRequired();

        builder
            .HasIndex(x => x.Name)
            .IsUnique();
    }
}