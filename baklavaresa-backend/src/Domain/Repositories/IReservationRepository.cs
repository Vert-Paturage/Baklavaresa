using Domain.Entities;

namespace Domain.Repositories;

public interface IReservationRepository
{
    Task Create(Reservation reservation);
    Task<Reservation> GetById(int id);
    Task<IList<Reservation>> GetAllForMonth(DateTime month);
    Task<IList<Reservation>> GetReservationsBySlot(DateTime slot, DateTime slotEnd);
    Task<IList<Reservation>> GetReservationsByDate(DateTime slot);
    Task Delete(Reservation reservation);
}