namespace SendMailExample.Models.DataModels;

public class Reservation
{
    public DateTime Date { get; set; }
    public int TableId { get; set; }
    public int NumberOfPeople { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}