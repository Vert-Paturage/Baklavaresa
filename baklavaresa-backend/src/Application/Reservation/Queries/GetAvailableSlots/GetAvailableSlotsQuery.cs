using Application.Reservation.Queries.GetAvailableSlotsByDay;
using Domain.Repositories;

namespace Application.Reservation.Queries.GetAvailableSlots;

public record GetAvailableSlotsQuery(int NumberOfPeople, DateTime Month): IRequest<IEnumerable<AvailableSlotsDto>>;

internal class GetAvailableSlotsQueryHandler(IReservationRepository reservationRepository, ITableRepository tableRepository) : IRequestHandler<GetAvailableSlotsQuery, IEnumerable<AvailableSlotsDto>>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly ITableRepository _tableRepository = tableRepository;
    public async Task<IEnumerable<AvailableSlotsDto>> Handle(GetAvailableSlotsQuery request, CancellationToken cancellationToken)
    {
        // get all reservations by day
        var reservations = await _reservationRepository.GetAllForMonth(request.Month);
        // deduce the empty tables
        var availableSlots = new List<AvailableSlotsDto>();
        return availableSlots;
    }
}