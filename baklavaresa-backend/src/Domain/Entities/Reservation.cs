namespace Domain.Entities;

public class Reservation 
{
    public Reservation(string firstName, string lastName, string email, DateTime schedule)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Schedule = schedule;
    }
    public Reservation(int id, string firstName, string lastName, string email, DateTime schedule)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Schedule = schedule;
    }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    //public int NumberOfPeople { get; set; }
    //public IList<Table> Tables { get; set; }
    public DateTime Schedule { get; set; }
}