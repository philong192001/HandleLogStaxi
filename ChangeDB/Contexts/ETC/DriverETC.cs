using ChangeDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChangeDB.Contexts.ETC;

public class DriverETC : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("Driver.Drivers");
        entityTypeBuilder.HasKey(p => p.PK_DriverId);
    }
}
