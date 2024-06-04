using Domain.Dates;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ReservationRepository(Persistence.DatabaseContext context) : IReservationRepository
{
    private readonly DatabaseContext _context = context;
    public Task<int> Create(Reservation reservation)
    {
        var dbReservation = new ReservationDatabase(reservation);
        _context.Reservations.Add(dbReservation);
        _context.SaveChanges();
        return Task.FromResult(dbReservation.Id);
    }
    public Task<Reservation> GetReservationById(int id)
    {
        var dbReservation = _context.Reservations
            .Include(r => r.Table)
            .FirstOrDefault(r => r.Id == id);
        if (dbReservation == null)
        {
            throw new Domain.Exceptions.Reservation.ReservationNotFoundException(id);
        }
        return Task.FromResult(dbReservation.ToDomainModel());
    }
    public Task<IList<Reservation>> GetReservationByMonth(BakMonth month)
    {
        var dbReservation = _context.Reservations
                .Include(r => r.Table)
                .Where(r => r.Date ==  month).ToList();
        return Task.FromResult<IList<Reservation>>(dbReservation.Select(r => r.ToDomainModel()).ToList());
    }

    public Task<IList<Reservation>> GetReservationsByDate(BakDay day)
    {
        var dbReservation = _context.Reservations
            .Include(r => r.Table)
            .Where(r => r.Date.Date == day.ToDateTime().Date).ToList();
        return Task.FromResult<IList<Reservation>>(dbReservation.Select(r => r.ToDomainModel()).ToList());
    }

     public Task<IList<Reservation>> GetReservationsBySlot(BakDate slotStart, BakDate slotEnd)
    {
        var dbReservation = _context.Reservations.
            Include(r => r.Table)
            .Where(r => r.Date >= (DateTime) slotStart && r.Date < slotEnd).ToList();
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

     public Task<List<Reservation>> GetReservationByTableId(int tableId)
    {
        var dbReservation = _context.Reservations.Include(r => r.Table).Where(r => r.TableId == tableId).ToList();
        return Task.FromResult(dbReservation.Select(r => r.ToDomainModel()).ToList());
    }

}