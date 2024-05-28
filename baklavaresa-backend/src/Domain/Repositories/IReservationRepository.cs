using Domain.Entities;

namespace Domain.Repositories;

public interface IReservationRepository
{
    public Task Create(Reservation reservation);
}