using API.Models.DataModels;

namespace API.Data;

public interface IDataManipulation
{
    public void InsertReservation(Reservation reservation);
    public void DeleteReservation(int id);
    public List<Table> GetTablesByCapacity(int capacity);
}