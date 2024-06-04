using Application.Services;
using Domain;
using Domain.Exceptions.Reservation;
using Domain.Repositories;

namespace Application.Reservation.Queries.GetAvailableSlots;

public record GetAvailableSlotsQuery(int NumberOfPeople, DateTime Month): IRequest<List<AvailableSlotsDto>>;

internal class GetAvailableSlotsQueryHandler(IReservationRepository reservationRepository, ITableRepository tableRepository, IClockService clockService) : IRequestHandler<GetAvailableSlotsQuery, List<AvailableSlotsDto>>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly ITableRepository _tableRepository = tableRepository;
    private readonly IClockService _clockService = clockService;
    public async Task<List<AvailableSlotsDto>> Handle(GetAvailableSlotsQuery request, CancellationToken cancellationToken)
    {
        if (request.NumberOfPeople <= 0)
        {
            throw new InvalidNumberOfPeopleException(request.NumberOfPeople);
        }
        var availableSlots = new List<AvailableSlotsDto>();
        var tables = await _tableRepository.GetAll();
        
        // get all the days in the month of the request except the days that have passed and the one where the restaurant is closed
        for (var day = 1; day <= DateTime.DaysInMonth(request.Month.Year, request.Month.Month); day++)
        {
            var date = new DateTime(request.Month.Year, request.Month.Month, day);
            if (date.Day < _clockService.Now.Day || !RestaurantInfo.OpenDays.Contains(date.DayOfWeek))
            {
                availableSlots.Add(
                    new AvailableSlotsDto()
                    {
                        Day = new DateTime(date.Year, date.Month, date.Day),
                        Slots = new List<DateTime>()
                    });
                continue;
            }
            var slots = new List<DateTime>();
            foreach (var hours in RestaurantInfo.LunchHours)
            {
                var slotDate = new DateTime(date.Year, date.Month, date.Day, hours.openingHour.Hours, 0, 0);
                while (slotDate.TimeOfDay < hours.closingHour)
                {
                    if (slotDate < _clockService.Now)
                    {
                        slotDate = slotDate.AddMinutes(RestaurantInfo.SlotsInterval);
                        continue;
                    }
                    var reservations = await _reservationRepository.GetReservationsBySlot(slotDate, slotDate.AddMinutes(RestaurantInfo.SlotsInterval));
                    // Get the list of reserved tables
                    var reservedTables = reservations.Select(r => r.Table.Id).ToList();
                    // Get all tables not in reservedTables
                    var availableTables = tables.Where(t => !reservedTables.Contains(t.Id)).ToList();
                    // Get all tables that can accommodate the number of people
                    var availableTablesForNumberOfPeople = availableTables.Where(t => t.Capacity >= request.NumberOfPeople).ToList();
                    // If no tables are available, skip to the next date
                    if (availableTablesForNumberOfPeople.Count == 0)
                    {
                        slotDate = slotDate.AddMinutes(RestaurantInfo.SlotsInterval);
                        continue;
                    };
                    slots.Add(slotDate);
                    slotDate = slotDate.AddMinutes(RestaurantInfo.SlotsInterval);
                }
            }
            availableSlots.Add(
                new AvailableSlotsDto()
                {
                    Day = new DateTime(date.Year, date.Month, date.Day),
                    Slots = slots
                });
        }
        
        return availableSlots;
    }
}