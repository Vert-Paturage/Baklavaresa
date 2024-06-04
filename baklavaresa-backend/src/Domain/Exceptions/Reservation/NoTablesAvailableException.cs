namespace Domain.Exceptions.Table;

public class NoTablesAvailableException: Exception
{
    public NoTablesAvailableException(DateTime requestDate, int requestNumberOfPeople)
        : base($"No tables available for {requestNumberOfPeople} people on {requestDate}")
    {
    } 
}