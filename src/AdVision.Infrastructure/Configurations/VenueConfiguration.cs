using AdVision.Domain.Venues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;

namespace AdVision.Infrastructure.Configurations;

// public sealed class VenueConfiguration : IEntityTypeConfiguration<Venue>
// {
//     public void Configure(EntityTypeBuilder<Venue> builder)
//     {
//         builder.ToTable("venues", table =>
//         {
//             table.HasCheckConstraint(
//                 "CK_Venue_Latitude",
//                 "\"latitude\" >= -90 AND \"latitude\" <= 90");
//
//             table.HasCheckConstraint(
//                 "CK_Venue_Longitude",
//                 "\"longitude\" >= -180 AND \"longitude\" <= 180");
//             
//             table.HasCheckConstraint(
//                 "CK_Venue_House_NotEmpty",
//                 "length(\"house\") > 0");
//             
//             table.HasCheckConstraint(
//                 "CK_Venue_Region_NotEmpty",
//                 "length(\"region\") > 10");
//             
//             table.HasCheckConstraint(
//                 "CK_Venue_District_NotEmpty",
//                 "length(\"district\") > 10");
//             
//             table.HasCheckConstraint(
//                 "CK_Venue_City_NotEmpty",
//                 "length(\"city\") > 10");
//             
//             table.HasCheckConstraint(
//                 "CK_Venue_Street_NotEmpty",
//                 "length(\"street\") > 10");
//             
//             table.HasCheckConstraint(
//                 "CK_Venue_Height",
//                 "\"height\" >= 100 AND \"height\" <= 10000");
//             
//             table.HasCheckConstraint(
//                 "CK_Venue_Width",
//                 "\"width\" >= 100 AND \"width\" <= 10000");
//         });
//         
//         builder.HasKey(x => x.Id);
//
//         builder
//             .Property(x => x.Id)
//             .HasColumnName("id")
//             .HasConversion(x => x.Value, name => new VenueId(name));
//         
//         builder
//             .Property(x => x.Name)
//             .HasConversion(
//                 v => v.Value,
//                 v => VenueName.Create(v).Value
//             )
//             .HasColumnName("name")
//             .HasMaxLength(LengthConstants.LENGTH_100)
//             .IsRequired();
//
//         builder.OwnsOne(x => x.Address, address =>
//         {
//             address.Property(x => x.Region)
//                 .HasColumnName("region")
//                 .HasMaxLength(300)
//                 .IsRequired();
//
//             address.Property(x => x.District)
//                 .HasColumnName("district")
//                 .HasMaxLength(300)
//                 .IsRequired();
//
//             address.Property(x => x.City)
//                 .HasColumnName("city")
//                 .HasMaxLength(300)
//                 .IsRequired();
//
//             address.Property(x => x.Street)
//                 .HasColumnName("street")
//                 .HasMaxLength(300)
//                 .IsRequired();
//
//             address.Property(x => x.House)
//                 .HasColumnName("house")
//                 .HasMaxLength(300)
//                 .IsRequired();
//
//             address.Property(x => x.Latitude)
//                 .HasColumnName("latitude")
//                 .IsRequired();
//
//             address.Property(x => x.Longitude)
//                 .HasColumnName("longitude")
//                 .IsRequired();
//         });
//
//         builder.OwnsOne(x => x.Size, size =>
//         {
//             size.Property(x => x.Height)
//                 .HasColumnName("height")
//                 .IsRequired();
//             
//             size.Property(x => x.Width)
//                 .HasColumnName("width")
//                 .IsRequired();
//         });
//         
//         builder
//             .Property(x => x.Name)
//             .HasConversion(
//                 v => v.Value,
//                 v => VenueName.Create(v).Value
//             )
//             .HasColumnName("name")
//             .HasMaxLength(LengthConstants.LENGTH_100)
//             .IsRequired();
//     }
// }