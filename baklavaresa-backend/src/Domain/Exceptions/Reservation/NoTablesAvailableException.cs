namespace Domain.Exceptions.Table;

public class NoTablesAvailableException: Exception
{
    public NoTablesAvailableException(DateTime requestDate, int requestNumberOfPeople)
        : base($"Il n'y a pas de table disponible pour {requestNumberOfPeople} personnes le {requestDate}.")
    {
    } 
}