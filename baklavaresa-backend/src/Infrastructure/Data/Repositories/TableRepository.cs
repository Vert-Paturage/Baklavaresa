using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Persistence;

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

    //delete table by id
    public Task Delete(int tableId)
    {
        var dbTable = _context.Tables.Find(tableId);
        if (dbTable == null)
        {
            throw new Domain.Exceptions.Table.TableNotFoundException(tableId);
        }
        _context.Tables.Remove(dbTable);
        return _context.SaveChangesAsync();
    }

    public Task<Table> GetAvailableTable(DateTime slot, int requestNumberOfPeople)
    {
        var reservations = _context.Reservations.Where(r => r.Date == slot).ToList();
        var reservedTables = reservations.Select(r => r.TableId).ToList();
        var availableTables = _context.Tables.Where(t => !reservedTables.Contains(t.Id)).ToList();
        var availableTablesForNumberOfPeople = availableTables.Where(t => t.Capacity >= requestNumberOfPeople).ToList();
        // sort by pertinence (smallest table that can accommodate the number of people)
        availableTablesForNumberOfPeople = availableTablesForNumberOfPeople.OrderBy(t => t.Capacity).ToList();
        if (availableTablesForNumberOfPeople.Count == 0)
        {
            throw new Domain.Exceptions.Table.NoTablesAvailableException(slot, requestNumberOfPeople);
        }
        return Task.FromResult(availableTablesForNumberOfPeople.OrderBy(t => t.Capacity).First().ToDomainModel());
    }
}