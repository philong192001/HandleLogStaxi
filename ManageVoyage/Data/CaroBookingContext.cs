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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder
        //    .UseSqlServer("Server=14.177.235.149,11023;Database=Staxi_G7;User Id=staxi_pmdh;Password=staxi@ba1234;Integrated Security=False;")
        //    .EnableSensitiveDataLogging(true); // Bật chế độ ghi log dữ liệu nhạy cảm
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CaroBookingEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CaroBookingProcessEntityTypeConfiguration());
    }
}