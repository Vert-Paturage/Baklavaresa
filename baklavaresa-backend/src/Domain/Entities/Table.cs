namespace Domain.Entities;

public class Table
{
    public int Id { get; set; }
    public int Capacity { get; set; }
    
    public Table(int id, int capacity)
    {
        Id = id;
        Capacity = capacity;
    }
    
    public Table() { }
    
    public Table(int capacity)
    {
        Capacity = capacity;
    }
}