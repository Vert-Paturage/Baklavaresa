using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;

namespace Infrastructure.Data;

public class DatabaseContext: DbContext, IDatabase
{
    public DatabaseContext() {}
    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options) { }
    
    public int SaveChanges(IList<IUpdateEntry> entries)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(IList<IUpdateEntry> entries, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Func<QueryContext, TResult> CompileQuery<TResult>(Expression query, bool async)
    {
        throw new NotImplementedException();
    }
}