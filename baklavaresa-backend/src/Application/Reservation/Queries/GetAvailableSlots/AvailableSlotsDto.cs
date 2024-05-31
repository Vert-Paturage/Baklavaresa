namespace Application.Reservation.Queries.GetAvailableSlots;

public class AvailableSlotsDto {
	public DateTime Day { get; set; }
	public IEnumerable<DateTime> Slots { get; set; }
}