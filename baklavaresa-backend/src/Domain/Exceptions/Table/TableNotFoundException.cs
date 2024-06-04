namespace Domain.Exceptions.Table;

public class TableNotFoundException: Exception
{
    public TableNotFoundException(int id) : base($"La table avec l'id {id} n'a pas été trouvée")
    {
    }
}