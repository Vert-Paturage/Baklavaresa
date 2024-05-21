using Models.DataModels;
using static Newtonsoft.Json.JsonConvert;

namespace Data;

public class DataManipulation
{
    public static List<Table> GetTablesByCapacity(int capacity)
    {
        var tables = DeserializeObject<List<Table>>(File.ReadAllText("Data/Tables.json"));
        return tables != null ? tables.Where(t => t.Capacity == capacity).ToList() : new List<Table>();
    }

	public static void InsertReservation(Reservation reservation)
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

		reservations.Add(reservation);
		File.WriteAllText("Data/Reservations.json", SerializeObject(reservations));
	}
}