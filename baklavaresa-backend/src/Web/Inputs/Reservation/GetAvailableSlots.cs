namespace Web.Inputs.Reservation;

public record GetAvailableSlots
{
    public int NumberOfPeople { get; set; }
    public DateTime Month { get; set; } 
}