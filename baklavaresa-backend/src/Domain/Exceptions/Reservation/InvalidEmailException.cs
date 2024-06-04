namespace Domain.Exceptions.Reservation;

public class InvalidEmailException(string email) : Exception($"Invalid email: {email}");