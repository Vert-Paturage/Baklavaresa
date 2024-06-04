using Domain.Repositories;
using System;

namespace Application.Table.Commands.DeleteTable;

public record DeleteTableCommand(int iD) : IRequest<Unit>;

public class DeleteTableCommandHandler(ITableRepository tableRepository, IReservationRepository reservationRepository) : IRequestHandler<DeleteTableCommand, Unit>
{
    private readonly ITableRepository _tableRepository = tableRepository;
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    public Task<Unit> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
    {
        var idTable = request.iD;
        List<Domain.Entities.Reservation> reservations = _reservationRepository.GetReservationByTableId(idTable).Result;
        if(reservations.Count > 0)
        {
            throw new Domain.Exceptions.Table.TableNotDeleteException();
        }
        _tableRepository.Delete(idTable);
        return Unit.Task;
    }
}
