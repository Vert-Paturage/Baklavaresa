using Models.DataModels;

namespace Data;

public interface IDataManipulation
{
    public void InsertReservation(Reservation reservation);
    public List<Table> GetTablesByCapacity(int capacity);
}