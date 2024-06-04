using Domain.Entities;

namespace Domain.Repositories;

public interface ITableRepository
{
    Task<IList<Table>> GetAll();
    Task<IList<Table>> GetTablesByCapacity(int requestNumberOfPeople);
    Table GetById(int requestTable);
    Task<int> Create(Table table);
    Task<Table> GetAvailableTable(DateTime slot, int requestNumberOfPeople);
    Task Delete(int tableId);
}