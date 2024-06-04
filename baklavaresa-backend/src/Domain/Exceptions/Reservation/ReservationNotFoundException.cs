namespace Domain.Exceptions.Reservation;

public class ReservationNotFoundException: Exception
{
    public ReservationNotFoundException(int id): base($"La réservation avec l'id {id} n'a pas été trouvée.")
    {
    } 
}