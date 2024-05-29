namespace Application.Reservation.Queries.GetAvailableSlotsByDay;

public class AvailableSlotsDto
{
    public string Day { get; set; }
    public IEnumerable<DateTime> Slots { get; set; }
}