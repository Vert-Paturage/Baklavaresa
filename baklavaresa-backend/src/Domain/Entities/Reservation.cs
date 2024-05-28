namespace Domain.Entities;

public class Reservation 
{
    public Reservation(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    public Reservation(int id, string firstName, string lastName, string email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    //public int NumberOfPeople { get; set; }
    //public IList<Table> Tables { get; set; }
    //public Schedule Schedule { get; set; }
}