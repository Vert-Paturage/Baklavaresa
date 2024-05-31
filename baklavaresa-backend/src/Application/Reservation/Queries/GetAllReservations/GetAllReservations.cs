using Domain.Entities;
using Domain.Repositories;

namespace Application.Reservation.Queries.GetAllReservations;

public record GetAllReservationsQuery(DateTime date): IRequest<IList<Domain.Entities.Reservation>>;

internal class GetAllReservationsQueryHandler(IReservationRepository reservationRepository) : IRequestHandler<GetAllReservationsQuery, IList<Domain.Entities.Reservation>>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    public Task<IList<Domain.Entities.Reservation>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
    {
        return _reservationRepository.GetReservationsByDate(request.date);
    }
}