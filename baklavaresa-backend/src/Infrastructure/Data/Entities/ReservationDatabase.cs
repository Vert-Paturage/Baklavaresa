using Domain.Entities;

namespace Infrastructure.Data.Entities;

internal class ReservationDatabase: DataEntity<Reservation>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime Date { get; set; }
    public TableDatabase Table { get; set; }
    public int NumberOfPeople { get; set; }
    
    public ReservationDatabase() { }
    public ReservationDatabase(Reservation domainModel) : base(domainModel)
    {
    }

    public override void FromDomainModel(Reservation domainModel)
    {
        Id = domainModel.Id;
        FirstName = domainModel.FirstName;
        LastName = domainModel.LastName;
        Email = domainModel.Email;
        Date = domainModel.Date;
        NumberOfPeople = domainModel.NumberOfPeople;
        Table = new TableDatabase(domainModel.Table);
    }

    public override Reservation ToDomainModel()
    {
        return new Reservation(Id, FirstName, LastName, Email, Date, NumberOfPeople, Table.ToDomainModel());
    }
}