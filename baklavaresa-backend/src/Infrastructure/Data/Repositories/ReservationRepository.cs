using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;

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
        var dbReservation = _context.Reservations
                .Include(r => r.Table)
                .Where(r => r.Date.Month == month.Month).ToList();
        return Task.FromResult<IList<Reservation>>(dbReservation.Select(r => r.ToDomainModel()).ToList());
    }

    public Task<IList<Reservation>> GetReservationsByDate(DateTime slot)
    {
        var dbReservation = _context.Reservations
            .Include(r => r.Table)
            .Where(r => r.Date.Date == slot.Date).ToList();
        return Task.FromResult<IList<Reservation>>(dbReservation.Select(r => r.ToDomainModel()).ToList());
    }

     public Task<IList<Reservation>> GetReservationsBySlot(DateTime slotStart, DateTime slotEnd)
    {
        var dbReservation = _context.Reservations.
            Include(r => r.Table)
            .Where(r => r.Date >= slotStart && r.Date < slotEnd).ToList();
        return Task.FromResult<IList<Reservation>>(dbReservation.Select(r => r.ToDomainModel()).ToList());
    }

    public Task Delete(int reservationID)
    {
        var dbReservation = _context.Reservations.Find(reservationID);
        if (dbReservation != null)
        {
            _context.Reservations.Remove(dbReservation);
            return _context.SaveChangesAsync();
        }
        throw new Domain.Exceptions.Reservation.ReservationNotFoundException(reservationID);
    }
}