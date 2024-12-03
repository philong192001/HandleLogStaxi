using ChangeDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChangeDB.Contexts.ETC
{
    public class FloorEmployeeHistoryResultETC : IEntityTypeConfiguration<FloorEmployeeHistoryResult>
    {
        public void Configure(EntityTypeBuilder<FloorEmployeeHistoryResult> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Floor.EmployeeHistoryResult");
            entityTypeBuilder.HasKey(p => p.Id);
        }
    }
}
