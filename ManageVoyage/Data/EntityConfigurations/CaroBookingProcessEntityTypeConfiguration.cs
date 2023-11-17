using ManageVoyage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageVoyage.Data.EntityConfigurations;

public class CaroBookingProcessEntityTypeConfiguration : IEntityTypeConfiguration<CaroBookingProcess>
{
    public void Configure(EntityTypeBuilder<CaroBookingProcess> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("CaroBooking_Process");
        entityTypeBuilder.HasKey(p => p.Id);
        entityTypeBuilder.Property(p => p.Id).HasColumnType("INT");
        entityTypeBuilder.Property(p => p.PhoneCustomer).HasColumnType("NVARCHAR(20)");
        entityTypeBuilder.Property(p => p.NameCustomer).HasColumnType("NVARCHAR(50)");
        entityTypeBuilder.Property(p => p.Status).HasColumnType("NVARCHAR(50)");
        entityTypeBuilder.Property(p => p.Line).HasColumnType("INT");
        entityTypeBuilder.Property(p => p.UpdatedByUser).HasColumnType("NVARCHAR(50)");
        entityTypeBuilder.Property(p => p.UpdatedDate).HasColumnType("DATETIME");
        entityTypeBuilder.Property(p => p.Note).HasColumnType("NVARCHAR(3000)");
        entityTypeBuilder.Property(p => p.InstallAppDate).HasColumnType("DATETIME");
        entityTypeBuilder.Property(p => p.FullName).HasColumnType("NVARCHAR(150)");
    }
}
