namespace Domain.Exceptions.Reservation;

public class ReservationNotFoundException: Exception
{
    public ReservationNotFoundException(int id): base($"Reservation with id {id} was not found.")
    {
    } 
}