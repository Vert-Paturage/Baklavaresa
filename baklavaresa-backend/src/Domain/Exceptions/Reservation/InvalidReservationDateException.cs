namespace Domain.Exceptions.Reservation;

public class InvalidReservationDateException(DateTime date) : Exception($"Invalid reservation date: {date}");