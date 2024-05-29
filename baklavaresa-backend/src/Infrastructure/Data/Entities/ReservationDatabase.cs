using Domain.Entities;

namespace Infrastructure.Data.Entities;

internal class ReservationDatabase: DataEntity<Reservation>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime Date { get; set; }
    public IList<TableDatabase> Tables { get; set; }
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
    }

    public override Reservation ToDomainModel()
    {
        var tables = Tables.Select(t => t.ToDomainModel()).ToList();
        return new Reservation(Id, FirstName, LastName, Email, Date, NumberOfPeople, tables);
    }
}