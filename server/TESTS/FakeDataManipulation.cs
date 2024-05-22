using API.Data;
using API.Models.DataModels;

namespace TESTS;

public class FakeDataManipulation: IDataManipulation
{
    public Reservation? LastReservation { get; private set; } = null;
    public void InsertReservation(Reservation reservation)
    {
        LastReservation = reservation; 
    }

    public List<Table> GetTablesByCapacity(int capacity)
    {
        throw new NotImplementedException();
    }
}