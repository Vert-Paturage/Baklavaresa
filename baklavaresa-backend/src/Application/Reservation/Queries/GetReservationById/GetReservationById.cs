using Domain.Repositories;

namespace Application.Reservation.Queries.GetReservationById;

public record GetReservationByIdQuery(int Id): IRequest<Domain.Entities.Reservation>;

internal class GetReservationHandler(IReservationRepository repository) : IRequestHandler<GetReservationByIdQuery, Domain.Entities.Reservation>
{
    private readonly IReservationRepository _repository = repository;
    public async Task<Domain.Entities.Reservation> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetById(request.Id);
    }
}