namespace Domain.Exceptions.Reservation;

public class InvalidNumberOfPeopleException(int numberOfPeople)
   : Exception($"Le nombre de personnes est invalide : {numberOfPeople}.");