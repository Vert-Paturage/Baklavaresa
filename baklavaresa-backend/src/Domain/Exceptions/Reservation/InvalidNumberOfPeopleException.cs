namespace Domain.Exceptions.Reservation;

public class InvalidNumberOfPeopleException(int numberOfPeople)
   : Exception($"Invalid number of people: {numberOfPeople}");