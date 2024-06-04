using Domain.Dates;
using Domain.Entities;

namespace Domain.Repositories;

public interface ITableRepository
{
    Task<IList<Table>> GetAll();
    Task<IList<Table>> GetTablesByCapacity(int requestNumberOfPeople);
    Task<Table> GetById(int requestTable);
    Task<int> Create(Table table);
    Task<List<Table>> GetAvailableTable(BakDate date, int requestNumberOfPeople);
    Task Delete(int tableId);
}