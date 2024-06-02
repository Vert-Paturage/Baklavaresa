using Domain.Repositories;
using System;

namespace Application.Table.Commands.DeleteTable;

public record DeleteTableCommand(int iD) : IRequest<Unit>;

public class DeleteTableCommandHandler(ITableRepository tableRepository) : IRequestHandler<DeleteTableCommand, Unit>
{
    private readonly ITableRepository _tableRepository = tableRepository;
    public Task<Unit> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
    {
        var idTable = request.iD;
        _tableRepository.Delete(idTable);
        return Unit.Task;
    }
}
