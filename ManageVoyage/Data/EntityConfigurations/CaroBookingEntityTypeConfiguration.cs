using ManageVoyage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageVoyage.Data.EntityConfigurations;

public class CaroBookingEntityTypeConfiguration : IEntityTypeConfiguration<CaroBooking>
{
    public void Configure(EntityTypeBuilder<CaroBooking> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("CaroBooking");
        entityTypeBuilder.HasKey(p => p.Id);
        entityTypeBuilder.Property(p => p.Id).HasColumnType("INT");
        entityTypeBuilder.Property(p => p.CreatedAt).HasColumnType("DECIMAL(18,0)");
        entityTypeBuilder.Property(p => p.SourceAddress).HasColumnType("NVARCHAR(500)");
        entityTypeBuilder.Property(p => p.ReturnAddress).HasColumnType("NVARCHAR(500)");
        entityTypeBuilder.Property(p => p.SourceLat).HasColumnType("FLOAT");
        entityTypeBuilder.Property(p => p.SourceLong).HasColumnType("FLOAT");
        entityTypeBuilder.Property(p => p.Status).HasColumnType("NVARCHAR(50)");
        entityTypeBuilder.Property(p => p.SourceApp).HasColumnType("NVARCHAR(50)");
        entityTypeBuilder.Property(p => p.Partner).HasColumnType("NVARCHAR(50)");
        entityTypeBuilder.Property(p => p.CarType).HasColumnType("NVARCHAR(50)");
        entityTypeBuilder.Property(p => p.TotalCash).HasColumnType("DECIMAL(18,0)");
        entityTypeBuilder.Property(p => p.PhoneCustomer).HasColumnType("NVARCHAR(20)");
        entityTypeBuilder.Property(p => p.NameCustomer).HasColumnType("NVARCHAR(50)");
    }
}
