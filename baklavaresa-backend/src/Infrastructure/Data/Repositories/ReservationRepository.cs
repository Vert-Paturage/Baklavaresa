using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Persistence;

namespace Infrastructure.Data.Repositories;

public class ReservationRepository(Persistence.DatabaseContext context) : IReservationRepository
{
    private readonly DatabaseContext _context = context;
    public Task Create(Reservation reservation)
    {
        var dbReservation = new ReservationDatabase(reservation);
        _context.Reservations.Add(dbReservation);
        return context.SaveChangesAsync();
    }
    public Task<Reservation> GetById(int id)
    {
        var dbReservation = _context.Reservations.Find(id);
        if (dbReservation == null)
        {
            throw new Domain.Exceptions.Reservation.ReservationNotFoundException(id);
        }
        return Task.FromResult(dbReservation.ToDomainModel());
    }
    public Task<IList<Reservation>> GetAllForMonth(DateTime month)
    {
        var dbReservation = _context.Reservations.Where(r => r.Date.Month == month.Month).ToList();
        return Task.FromResult<IList<Reservation>>(dbReservation.Select(r => r.ToDomainModel()).ToList());
    }

    public Task<IList<Reservation>> GetReservationsByDate(DateTime slot)
    {
        var slotEnd = slot.AddHours(1);
        var dbReservation = _context.Reservations.Where(r => r.Date >= slot && r.Date < slotEnd).ToList();
        return Task.FromResult<IList<Reservation>>(dbReservation.Select(r => r.ToDomainModel()).ToList());
    }
}