using HandleLog.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HandleLog.Contexts;

public class LandingPageManagementDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; }

    public LandingPageManagementDbContext(DbContextOptions<LandingPageManagementDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
    }
}