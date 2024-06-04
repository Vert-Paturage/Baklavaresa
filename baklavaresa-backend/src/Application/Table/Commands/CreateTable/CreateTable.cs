using Domain.Repositories;
using System;
using Domain.Exceptions.Table;

namespace Application.Table.Commands.CreateTable;

public record CreateTableCommand(int Capacity) : IRequest<int>;

public class CreateTableCommandHandler(ITableRepository tableRepository) : IRequestHandler<CreateTableCommand, int>
{
    private readonly ITableRepository _tableRepository = tableRepository;
    public Task<int> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        if (request.Capacity <= 0)
        {
            throw new InvalidTableCapacity(request.Capacity);
        }
        var table = new Domain.Entities.Table(request.Capacity);
        var id = _tableRepository.Create(table);
        return id;
    }
}
