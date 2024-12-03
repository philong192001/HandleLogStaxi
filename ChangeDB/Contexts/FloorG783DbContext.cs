using ChangeDB.Contexts.ETC;
using ChangeDB.Models;
using Microsoft.EntityFrameworkCore;

namespace ChangeDB.Contexts;

public class FloorG783DbContext : DbContext
{
    public DbSet<CompArticle> AdminTrips { get; set; }
    public DbSet<EmployeeShift> EmployeeShifts { get; set; }
    public DbSet<CustBooksLobby> CustBooksLobbies { get; set; }
    public DbSet<FloorEmployeeHistory> FloorEmployeeHistories { get; set; }
    public DbSet<FloorEmployeeHistoryResult> FloorEmployeeHistoryResults { get; set; }

    public FloorG783DbContext(DbContextOptions<FloorG783DbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdminTripETConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeShiftETC());
        modelBuilder.ApplyConfiguration(new CustBooksLobbyETC());
        modelBuilder.ApplyConfiguration(new EmployeeHistoryETC());
        modelBuilder.ApplyConfiguration(new FloorEmployeeHistoryResultETC());

    }
}
