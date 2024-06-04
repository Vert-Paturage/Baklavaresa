namespace Domain.Exceptions.Reservation;

public class InvalidReservationDateException(DateTime date) : Exception($"La date de réservation est invalide : {date}.");