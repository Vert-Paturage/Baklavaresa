using Domain.Entities;

namespace Domain.Repositories;

public interface ITableRepository
{
    Task<IList<Table>> GetAll();
    Task<IList<Table>> GetTablesByCapacity(int requestNumberOfPeople);
    Table GetById(int requestTable);
    Task Create(Table table);
}