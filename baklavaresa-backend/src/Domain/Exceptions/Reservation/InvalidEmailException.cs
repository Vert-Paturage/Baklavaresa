namespace Domain.Exceptions.Reservation;

public class InvalidEmailException(string email) : Exception($"Email {email} invalide.");