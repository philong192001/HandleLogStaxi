using HandleLog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandleLog.Contexts;

public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("Company");
        entityTypeBuilder.HasKey(p => p.Id);
        entityTypeBuilder.Property(p => p.Id).HasColumnType("INT");
        entityTypeBuilder.Property(p => p.Name).HasColumnType("NVARCHAR(150)");
        entityTypeBuilder.Property(p => p.LinkTest).HasColumnType("NVARCHAR(200)");
        entityTypeBuilder.Property(p => p.Note).HasColumnType("NVARCHAR(150)");
        entityTypeBuilder.Property(p => p.CreatedDate).HasColumnType("DATETIME");
        entityTypeBuilder.Property(p => p.UpdatedDate).HasColumnType("DATETIME");
        entityTypeBuilder.Property(p => p.Status).HasColumnType("INT");
        entityTypeBuilder.Property(p => p.LastUpdate).HasColumnType("DATETIME");
        entityTypeBuilder.Property(p => p.LinkRelease).HasColumnType("NVARCHAR(200)");
        entityTypeBuilder.Property(p => p.Port).HasColumnType("NVARCHAR(10)");
        entityTypeBuilder.Property(p => p.XnCode).HasColumnType("NVARCHAR(10)");
        entityTypeBuilder.Property(p => p.ParentCompanyID).HasColumnType("INT");
        entityTypeBuilder.Property(p => p.CompanyId).HasColumnType("INT");
        entityTypeBuilder.Property(p => p.Province).HasColumnType("NVARCHAR(50)");
        entityTypeBuilder.Property(p => p.CreatedByUser).HasColumnType("NVARCHAR(50)");
        entityTypeBuilder.Property(p => p.UpdatedByUser).HasColumnType("NVARCHAR(50)");
        entityTypeBuilder.Property(p => p.IsDeleted).HasColumnType("BIT");
        entityTypeBuilder.Property(p => p.CompanyName).HasColumnType("NVARCHAR(100)");
        entityTypeBuilder.Property(p => p.BusinessName).HasColumnType("NVARCHAR(50)");
        entityTypeBuilder.Property(p => p.BusinessStatus).HasColumnType("INT");
        entityTypeBuilder.Property(p => p.OperatorAccount).HasColumnType("NVARCHAR(50)");
        entityTypeBuilder.Property(p => p.AppChargeDate).HasColumnType("DATETIME");
        entityTypeBuilder.Property(p => p.MapChargeDate).HasColumnType("DATETIME");
        entityTypeBuilder.Property(p => p.RegionId).HasColumnType("NVARCHAR(500)");
        entityTypeBuilder.Property(p => p.ServicePrice).HasColumnType("DECIMAL(18,0)");
        entityTypeBuilder.Property(p => p.MapPrice).HasColumnType("DECIMAL(18,0)");
        entityTypeBuilder.Property(p => p.ServiceMailTo).HasColumnType("NVARCHAR(500)");
        entityTypeBuilder.Property(p => p.ServiceMailCC).HasColumnType("NVARCHAR(500)");
        entityTypeBuilder.Property(p => p.IsMiniCompany).HasColumnType("BIT");
        entityTypeBuilder.Property(p => p.IPServerFTP).HasColumnType("NVARCHAR(50)");
        entityTypeBuilder.Property(p => p.PortFTP).HasColumnType("INT");
        entityTypeBuilder.Property(p => p.UserNameFTP).HasColumnType("NVARCHAR(20)");
        entityTypeBuilder.Property(p => p.PasswordFTP).HasColumnType("NVARCHAR(20)");
        entityTypeBuilder.Property(p => p.DirectoryFTP).HasColumnType("NVARCHAR(200)");
        entityTypeBuilder.Property(p => p.DirectoryLog).HasColumnType("NVARCHAR(200)");
    }
}