using Domain.Repositories;
using System;

namespace Application.Table.Commands.CreateTable;

public record CreateTableCommand(int Capacity) : IRequest<Unit>;

public class CreateTableCommandHandler(ITableRepository tableRepository) : IRequestHandler<CreateTableCommand, Unit>
{
    private readonly ITableRepository _tableRepository = tableRepository;
    public Task<Unit> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        var table = new Domain.Entities.Table(request.Capacity);
        _tableRepository.Create(table);
        return Unit.Task;
    }
}
