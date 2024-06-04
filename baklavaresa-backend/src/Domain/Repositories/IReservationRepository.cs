using Domain.Entities;

namespace Domain.Repositories;

public interface IReservationRepository
{
    Task<int> Create(Reservation reservation);
    Task Delete(int reservationID);
    Task<Reservation> GetReservationById(int id);
    Task<IList<Reservation>> GetReservationByMonth(DateTime month);
    Task<IList<Reservation>> GetReservationsBySlot(DateTime slot, DateTime slotEnd);
    Task<IList<Reservation>> GetReservationsByDate(DateTime slot);
    Task<List<Reservation>> GetReservationByTableId(int tableId);
}