using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data.Repositories;

public class TableRepository(DatabaseContext context) : ITableRepository
{
    private readonly DatabaseContext _context = context;
    public Task<IList<Table>> GetAll()
    {
        var dbTables = _context.Tables.ToList();
        return Task.FromResult<IList<Table>>(dbTables.Select(t => t.ToDomainModel()).ToList());
    }

    public Task<IList<Table>> GetTablesByCapacity(int requestNumberOfPeople)
    {
        var dbTables = _context.Tables.Where(t => t.Capacity >= requestNumberOfPeople).ToList();
        return Task.FromResult<IList<Table>>(dbTables.Select(t => t.ToDomainModel()).ToList());
    }

    public Table GetById(int requestTable)
    {
        var dbTable = _context.Tables.Find(requestTable);
        if (dbTable == null)
        {
            throw new Domain.Exceptions.Table.TableNotFoundException(requestTable);
        }
        return dbTable.ToDomainModel();
    }

    public Task Create(Table table)
    {
        var dbTable = new TableDatabase(table);
        _context.Tables.Add(dbTable);
        return context.SaveChangesAsync();
    }
}