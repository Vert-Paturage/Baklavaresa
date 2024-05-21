using Models.DataModels;
using static Newtonsoft.Json.JsonConvert;

namespace Data;

public class Data
{
    public static List<Table> GetTablesByCapacity(int capacity)
    {
        var tables = DeserializeObject<List<Table>>(File.ReadAllText("Data/Tables.json"));
        return tables != null ? tables.Where(t => t.Capacity == capacity).ToList() : new List<Table>();
    }
}