using ManageVoyage.Data.EntityConfigurations;
using ManageVoyage.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageVoyage.Data;

public class CaroBookingContext : DbContext
{
    public DbSet<CaroBooking> CaroBookings { get; set; }
    public DbSet<CaroBookingProcess> CaroBookingProcesses { get; set; }

    public CaroBookingContext(DbContextOptions<CaroBookingContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CaroBookingEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CaroBookingProcessEntityTypeConfiguration());
    }
}
