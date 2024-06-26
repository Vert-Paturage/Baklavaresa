using Domain.Dates;
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

    public Task<Table> GetById(int requestTable)
    {
        var dbTable = _context.Tables.Find(requestTable);
        if (dbTable == null)
        {
            throw new Domain.Exceptions.Table.TableNotFoundException(requestTable);
        }

        return Task.FromResult(dbTable.ToDomainModel());
    }

    public Task<int> Create(Table table)
    {
        var dbTable = new TableDatabase(table);
        _context.Tables.Add(dbTable);
        context.SaveChanges();
        return Task.FromResult(dbTable.Id);
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

    public Task<List<Table>> GetAvailableTable(BakDate date, int requestNumberOfPeople)
    {
        var reservations = _context.Reservations.Where(r => r.Date == (DateTime)date).ToList();
        var reservedTables = reservations.Select(r => r.TableId).ToList();
        var availableTables = _context.Tables.Where(t => !reservedTables.Contains(t.Id)).ToList();
        var availableTablesForNumberOfPeople = availableTables.Where(t => t.Capacity >= requestNumberOfPeople).ToList();
        // sort by pertinence (smallest table that can accommodate the number of people)
        availableTablesForNumberOfPeople = availableTablesForNumberOfPeople.OrderBy(t => t.Capacity).ToList();
        return Task.FromResult(availableTablesForNumberOfPeople.Select(t => t.ToDomainModel()).ToList());
    }
}