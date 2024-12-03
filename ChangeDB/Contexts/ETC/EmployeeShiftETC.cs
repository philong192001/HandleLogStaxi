using ChangeDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChangeDB.Contexts.ETC;

public class EmployeeShiftETC : IEntityTypeConfiguration<EmployeeShift>
{
    public void Configure(EntityTypeBuilder<EmployeeShift> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("Employee.Shift");
        entityTypeBuilder.HasKey(p => p.Id);
    }
}
