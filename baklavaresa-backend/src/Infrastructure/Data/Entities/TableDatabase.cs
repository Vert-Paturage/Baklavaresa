using Domain.Entities;

namespace Infrastructure.Data.Entities;

internal class TableDatabase: DataEntity<Table>
{
    public int Id { get; set; }
    public int Capacity { get; set; }
    public ReservationDatabase Reservation { get; set; }
    public int ReservationId { get; set; } 
    public TableDatabase() { }
    public TableDatabase(Table domainModel) : base(domainModel)
    {
    }
    public override void FromDomainModel(Table domainModel)
    {
        Id = domainModel.Id;
        Capacity = domainModel.Capacity;
    }

    public override Table ToDomainModel()
    {
        return new Table(Id, Capacity);
    }
}