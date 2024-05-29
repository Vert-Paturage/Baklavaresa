namespace Domain.Entities;

public class Reservation 
{
    public Reservation() { }
    public Reservation(string firstName, string lastName, string email, DateTime date, int numberOfPeople, IList<Table> tables)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Date = date;
        NumberOfPeople = numberOfPeople;
        Tables = tables;
    }
    public Reservation(int id, string firstName, string lastName, string email, DateTime date, int numberOfPeople, IList<Table> tables)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Date = date;
        NumberOfPeople = numberOfPeople;
        Tables = tables;
    }
    
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int NumberOfPeople { get; set; }
    public DateTime Date { get; set; }
    public IList<Table> Tables { get; set; } = new List<Table>();
}