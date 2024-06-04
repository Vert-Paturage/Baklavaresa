namespace Application.Reservation.Queries.GetAllReservations;

public class AllReservationsDto {
    public int ID { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime Date { get; set; }
    public int NumberOfPeople { get; set; }
    public int Table { get; set; }
}