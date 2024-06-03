namespace Domain.Exceptions.Table;

public class TableNotDeleteException: Exception
{
    public TableNotDeleteException(): base($"La table ne peut pas être supprimée car elle a des réservations associées.")
    {
    } 
}