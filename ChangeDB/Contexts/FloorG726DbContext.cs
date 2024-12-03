using ChangeDB.Contexts.ETC;
using ChangeDB.Models;
using Microsoft.EntityFrameworkCore;

namespace ChangeDB.Contexts;

public class FloorG726DbContext : DbContext
{
    public DbSet<CompArticle> AdminTrips { get; set; }
    public DbSet<EmployeeShift> EmployeeShifts { get; set; }
    public DbSet<CustBooksLobby> CustBooksLobbies { get; set; }
    public DbSet<FloorEmployeeHistory> FloorEmployeeHistories { get; set; }
    public DbSet<DriverAssignCar> DriverAssignCars { get; set; }
    public DbSet<FloorEmployeeHistoryResult> FloorEmployeeHistoryResults { get; set; }
    public DbSet<CompArticle> CompArticles { get; set; }
    public DbSet<Driver> Drivers { get; set; }

    public FloorG726DbContext(DbContextOptions<FloorG726DbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdminTripETConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeShiftETC());
        modelBuilder.ApplyConfiguration(new CustBooksLobbyETC());
        modelBuilder.ApplyConfiguration(new EmployeeHistoryETC());
        modelBuilder.ApplyConfiguration(new FloorEmployeeHistoryResultETC());
        modelBuilder.ApplyConfiguration(new CompArticleETC());
        modelBuilder.ApplyConfiguration(new DriverETC());
        modelBuilder.ApplyConfiguration(new DriverAssignCarConfiguration());

    }
}
