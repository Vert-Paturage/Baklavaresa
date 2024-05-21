namespace Models.DataModels;

public class Reservation
{
	public int ID { get; set; }
    public Schedule Date { get; set; }
    public int NumberOfPeople { get; set; }
	public List<Table> Tables { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}