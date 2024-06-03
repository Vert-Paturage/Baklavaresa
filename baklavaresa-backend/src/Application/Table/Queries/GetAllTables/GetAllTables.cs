using Domain.Entities;
using Domain.Repositories;

namespace Application.Reservation.Queries.GetAllTables;

public record GetAllTablesQuery(): IRequest<IList<Domain.Entities.Table>>;

internal class GetAllReservationsQueryHandler(ITableRepository tableRepository) : IRequestHandler<GetAllTablesQuery, IList<Domain.Entities.Table>>
{
    private readonly ITableRepository _tableRepository = tableRepository;
    public async Task<IList<Domain.Entities.Table>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
    {
        return await _tableRepository.GetAll();
    }
}