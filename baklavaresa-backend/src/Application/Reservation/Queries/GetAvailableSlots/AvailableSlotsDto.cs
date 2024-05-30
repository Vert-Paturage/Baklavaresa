namespace Application.Reservation.Queries.GetAvailableSlotsByDay;

public class AvailableSlotsDto
{
    public DateTime Day { get; set; }
    public IEnumerable<DateTime> Slots { get; set; }
    public IDictionary<DateTime, IList<int>> Tables { get; set; }
}