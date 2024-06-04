using Domain.Dates;
using Domain.Entities;

namespace Domain.Repositories;

public interface IReservationRepository
{
    Task<int> Create(Reservation reservation);
    Task Delete(int reservationID);
    Task<Reservation> GetReservationById(int id);
    Task<IList<Reservation>> GetReservationByMonth(BakMonth month);
    Task<IList<Reservation>> GetReservationsBySlot(BakDate slot, BakDate slotEnd);
    Task<IList<Reservation>> GetReservationsByDate(BakDay day);
    Task<List<Reservation>> GetReservationByTableId(int tableId);
}