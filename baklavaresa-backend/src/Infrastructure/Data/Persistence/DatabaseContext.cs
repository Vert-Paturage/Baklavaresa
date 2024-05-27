using Domain.Entities;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;

namespace Infrastructure.Data.Persistence;

public class DatabaseContext: DbContext 
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
}