using AdVision.Domain.Contracts;
using AdVision.Domain.Customers;
using AdVision.Domain.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdVision.Infrastructure.Configurations;

public sealed class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("contracts");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new ContractId(value)
            );

        builder
            .Property(x => x.Number)
            .HasConversion(
                v => v.Value,
                v => ContractNumber.Create(v).Value
            )
            .HasColumnName("number")
            .HasMaxLength(ContractNumber.MAX_LENGTH)
            .IsRequired();

        builder
            .Property(x => x.CustomerId)
            .HasColumnName("customer_id")
            .HasConversion(
                id => id.Value,
                value => new CustomerId(value)
            )
            .IsRequired();

        builder
            .Property(x => x.EmployeeId)
            .HasColumnName("employee_id")
            .HasConversion(
                id => id.Value,
                value => new EmployeeId(value)
            )
            .IsRequired();

        builder.OwnsOne(x => x.DateInterval, dateIntervalBuilder =>
        {
            dateIntervalBuilder
                .Property(x => x.StartDate)
                .HasColumnName("start_date")
                .IsRequired();

            dateIntervalBuilder
                .Property(x => x.EndDate)
                .HasColumnName("end_date")
                .IsRequired();
        });

        builder
            .Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder
            .Property(x => x.SignedDate)
            .HasColumnName("signed_date")
            .IsRequired(false);

        builder
            .HasIndex(x => x.Number)
            .IsUnique();

        builder
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}