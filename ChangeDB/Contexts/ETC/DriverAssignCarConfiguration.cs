using ChangeDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChangeDB.Contexts.ETC;

public class DriverAssignCarConfiguration : IEntityTypeConfiguration<DriverAssignCar>
{
    public void Configure(EntityTypeBuilder<DriverAssignCar> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("Driver.AssignCar");
        // Cấu hình hai thuộc tính là khóa chính phức hợp
        entityTypeBuilder.HasKey(p => new { p.FK_DriverID, p.FK_VehicleID });
        entityTypeBuilder.Property(p => p.FK_DriverID).HasColumnType("INT").ValueGeneratedNever(); ;
        entityTypeBuilder.Property(p => p.FK_VehicleID).HasColumnType("INT").ValueGeneratedNever(); ;
        entityTypeBuilder.Property(p => p.FK_CompanyID).HasColumnType("INT");
        entityTypeBuilder.Property(p => p.VehiclePlate).HasColumnType("NVARCHAR(50)");
        entityTypeBuilder.Property(p => p.PrivateCode).HasColumnType("NVARCHAR(50)");
        entityTypeBuilder.Property(p => p.AssignedDate).HasColumnType("DATETIME");
    }
}
