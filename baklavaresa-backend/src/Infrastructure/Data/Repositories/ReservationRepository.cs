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
}