using AdVision.Domain;
using AdVision.Domain.Employees;
using AdVision.Domain.Positions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdVision.Infrastructure.Configurations;

public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new EmployeeId(value)
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

        builder
            .Property(x => x.Address)
            .HasConversion(
                v => v.Value,
                v => EmployeeAddress.Create(v).Value
            )
            .HasColumnName("address")
            .HasMaxLength(EmployeeAddress.MAX_LENGTH)
            .IsRequired();

        builder
            .Property(x => x.PositionId)
            .HasColumnName("position_id")
            .HasConversion(
                id => id.Value,
                value => new PositionId(value)
            )
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

        var passportBuilder = builder.OwnsOne(x => x.Passport);

        passportBuilder
            .Property(x => x.Series)
            .HasConversion(
                v => v.Value,
                v => PassportSeries.Create(v).Value
            )
            .HasColumnName("passport_series")
            .HasMaxLength(PassportSeries.LENGTH)
            .IsRequired();

        passportBuilder
            .Property(x => x.Number)
            .HasConversion(
                v => v.Value,
                v => PassportNumber.Create(v).Value
            )
            .HasColumnName("passport_number")
            .HasMaxLength(PassportNumber.LENGTH)
            .IsRequired();

        passportBuilder
            .HasIndex(x => new { x.Series, x.Number })
            .IsUnique();

        builder
            .HasOne<Position>()
            .WithMany()
            .HasForeignKey(x => x.PositionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}