namespace Domain.Exceptions.Table;

public class TableNotDeleteException: Exception
{
    public TableNotDeleteException(): base($"Table could not be deleted.")
    {
    } 
}