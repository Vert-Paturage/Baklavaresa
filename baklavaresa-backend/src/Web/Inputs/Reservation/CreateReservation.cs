using Domain.Entities;

namespace Web.Inputs.Reservation;

public class CreateReservation
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; } 
    public DateTime Date { get; set; }    
    public int Table { get; set; }
    public int NumberOfPeople { get; set; }
}