namespace Application.Reservation.Queries.GetAvailableSlotsByDay;

public class AvailableSlotsByDayDto
{
    public string Day { get; set; }
    public IEnumerable<DateTime> Slots { get; set; }
}