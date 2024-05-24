namespace Domain.Entities;

public class Reservation 
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int NumberOfPeople { get; set; }
    public IList<Table> Tables { get; set; }
    public Schedule Schedule { get; set; }
}