using Domain.Repositories;

namespace Application.Reservation.Queries.GetAvailableSlotsByDay;

public record GetAvailableSlotByDayQuery(int NumberOfPeople, DateTime Day): IRequest<IEnumerable<AvailableSlotsByDayDto>>;

internal class GetAvailableSlotByDayQueryHandler(IReservationRepository reservationRepository, ITableRepository tableRepository) : IRequestHandler<GetAvailableSlotByDayQuery, IEnumerable<AvailableSlotsByDayDto>>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly ITableRepository _tableRepository = tableRepository;
    public async Task<IEnumerable<AvailableSlotsByDayDto>> Handle(GetAvailableSlotByDayQuery request, CancellationToken cancellationToken)
    {
        // get all reservations by day
        var reservations = await _reservationRepository.GetAllByDay(request.Day);
        // deduce the empty tables
        var emptyTables = await _tableRepository.GetEmptyTablesByDay(request.Day);
        var availableSlots = new List<AvailableSlotsByDayDto>();
        return availableSlots;
    }
}