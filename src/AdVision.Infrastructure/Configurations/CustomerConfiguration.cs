using AdVision.Domain;
using AdVision.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdVision.Infrastructure.Configurations;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new CustomerId(value)
            );

        builder
            .Property(x => x.LastName)
            .HasConversion(
                v => v.Value,
                v => PersonName.Create(v).Value
            )
            .HasColumnName("last_name")
            .HasMaxLength(PersonName.MAX_LENGTH)
            .IsRequired();

        builder
            .Property(x => x.FirstName)
            .HasConversion(
                v => v.Value,
                v => PersonName.Create(v).Value
            )
            .HasColumnName("first_name")
            .HasMaxLength(PersonName.MAX_LENGTH)
            .IsRequired();

        builder
            .Property(x => x.MiddleName)
            .HasConversion(
                v => v.Value,
                v => PersonName.Create(v).Value
            )
            .HasColumnName("middle_name")
            .HasMaxLength(PersonName.MAX_LENGTH)
            .IsRequired();

        builder.OwnsOne(x => x.PhoneNumber, phoneBuilder =>
        {
            phoneBuilder
                .Property(x => x.Value)
                .HasColumnName("phone_number")
                .HasMaxLength(PhoneNumber.MAX_LENGTH)
                .IsRequired();

            phoneBuilder
                .HasIndex(x => x.Value)
                .IsUnique();
        });
    }
}