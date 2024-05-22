using API.Models.DataModels;
using static Newtonsoft.Json.JsonConvert;

namespace API.Data;

public class DataManipulation : IDataManipulation
{
    public List<Table> GetTablesByCapacity(int capacity)
    {
        var tables = DeserializeObject<List<Table>>(File.ReadAllText("Data/Tables.json"));
        return tables != null ? tables.Where(t => t.Capacity == capacity).ToList() : new List<Table>();
    }

	public  void InsertReservation(Reservation reservation)
	{
		var reservations = DeserializeObject<List<Reservation>>(File.ReadAllText("Data/Reservations.json"));
		if (reservations == null)
		{
			reservations = new List<Reservation>();
		}

		//check if reservation already exists
		if (reservations.Any(r => r.Date == reservation.Date && r.Date == reservation.Date))
		{
			throw new Exception("Reservation already exists");
		}
		//get the last id
		reservation.ID = reservations.Count > 0 ? reservations.Max(r => r.ID) + 1 : 1;
		reservations.Add(reservation);
		File.WriteAllText("Data/Reservations.json", SerializeObject(reservations));
	}
	public void DeleteReservation(int id)
	{
		var reservations = DeserializeObject<List<Reservation>>(File.ReadAllText("Data/Reservations.json"));
		if (reservations == null)
		{
			reservations = new List<Reservation>();
		}

		//check if reservation already exists
		if (reservations.Any(r => r.ID == id))
		{
			reservations.Remove(reservations.First(r => r.ID == id));
		}

		File.WriteAllText("Data/Reservations.json", SerializeObject(reservations));
	}
}