using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Infrastructure.Data.Persistence;

public class DatabaseContext: DbContext, IDatabase
{
    public DatabaseContext() { }

    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
    internal DbSet<ReservationDatabase> Reservations { get; set; }
    internal DbSet<TableDatabase> Tables { get; set; }
    public void Clear()
    {
        Reservations.RemoveRange(Reservations);
        Tables.RemoveRange(Tables);
        SaveChanges();
        
        ChangeTracker.Clear();
        Database.EnsureDeleted();
    }
}