using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data.Repositories;

public class ReservationRepository(Persistence.DatabaseContext context) : IReservationRepository
{
    public Task Create(Reservation reservation)
    {
        context.Reservations.Add(reservation);
        return context.SaveChangesAsync();
    }
}