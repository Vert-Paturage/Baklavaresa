using Domain.Entities;
using Domain.Repositories;

namespace Application.Reservation.Queries.GetAllReservations;

public record GetAllReservationsQuery(DateTime date): IRequest<IList<AllReservationsDto>>;

internal class GetAllReservationsQueryHandler(IReservationRepository reservationRepository) : IRequestHandler<GetAllReservationsQuery, IList<AllReservationsDto>>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    public async Task<IList<AllReservationsDto>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
    {
        var slotDate = new DateTime(request.date.Year, request.date.Month, request.date.Day, 0, 0, 0);
        var reservations = await _reservationRepository.GetReservationsByDateAdmin(slotDate);

        return reservations.Select(r => new AllReservationsDto
        {
            ID = r.Id,
            FirstName = r.FirstName,
            LastName = r.LastName,
            Email = r.Email,
            Date = r.Date,
            NumberOfPeople = r.NumberOfPeople,
            Table = r.Table.Id
        }).ToList();
    }
}