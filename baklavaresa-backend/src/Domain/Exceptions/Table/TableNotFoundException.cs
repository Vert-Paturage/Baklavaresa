namespace Domain.Exceptions.Table;

public class TableNotFoundException: Exception
{
    public TableNotFoundException(int id) : base($"Table with id {id} was not found.")
    {
    }
}