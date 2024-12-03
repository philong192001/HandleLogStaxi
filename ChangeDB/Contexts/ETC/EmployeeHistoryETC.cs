using ChangeDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChangeDB.Contexts.ETC
{
    public class EmployeeHistoryETC : IEntityTypeConfiguration<FloorEmployeeHistory>
    {
        public void Configure(EntityTypeBuilder<FloorEmployeeHistory> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Floor.EmployeeHistory");
            entityTypeBuilder.Property(p => p.Id).HasColumnType("uniqueidentifier").ValueGeneratedOnAdd();
            entityTypeBuilder.HasKey(p => p.Id);
        }
    }
}
