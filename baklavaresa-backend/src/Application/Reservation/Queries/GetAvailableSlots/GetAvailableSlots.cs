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
        var availableSlots = new List<AvailableSlotsDto>();
        var tablesForSlots = new Dictionary<DateTime, IList<int>>();
        var tables = await _tableRepository.GetAll();
        for (var day = 1; day <= DateTime.DaysInMonth(request.Month.Year, request.Month.Month); day++)
        {
            var slots = new List<DateTime>();
            var tablesForDay = new List<int>();
            for (var hour = 10; hour <= 20; hour++)
            {
                var slotDate = new DateTime(request.Month.Year, request.Month.Month, day, hour, 0, 0);
                var reservations = await _reservationRepository.GetReservationsByDate(slotDate);
                // Get the list of reserved tables
                var reservedTables = reservations.Select(r => r.Table.Id).ToList();
                // Get all tables not in reservedTables
                var availableTables = tables.Where(t => !reservedTables.Contains(t.Id)).ToList();
                // Get all tables that can accommodate the number of people
                var availableTablesForNumberOfPeople = availableTables.Where(t => t.Capacity >= request.NumberOfPeople).ToList();
                // If no tables are available, skip to the next date
                if (availableTablesForNumberOfPeople.Count == 0) continue;
                // the most pertinent table is the one with the smallest capacity
                availableTablesForNumberOfPeople = availableTablesForNumberOfPeople.OrderBy(t => t.Capacity).ToList();
                tablesForSlots[slotDate] = availableTablesForNumberOfPeople.Select(t => t.Id).ToList();
                slots.Add(new DateTime(request.Month.Year, request.Month.Month, day, hour, 0, 0));
            }  
            availableSlots.Add(
                new AvailableSlotsDto()
                {
                    Day = new DateTime(request.Month.Year, request.Month.Month, day),
                    Slots = slots, 
                    Tables = tablesForSlots
                });
        }
        return availableSlots;
    }
}