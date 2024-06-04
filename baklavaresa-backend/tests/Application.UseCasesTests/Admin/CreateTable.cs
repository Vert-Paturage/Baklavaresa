using Application.Table.Commands.CreateTable;
using Domain.Exceptions.Table;
using Domain.Repositories;
using Infrastructure.Data.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCasesTests.Admin;

public class CreateTable: IClassFixture<Dependencies>, IDisposable
{
    private readonly IDatabase  _database;
    private readonly ITableRepository _tableRepository;
    
    public CreateTable(Dependencies dependencies)
    {
        _database = dependencies.ServiceProvider.GetRequiredService<IDatabase>();
        _tableRepository = dependencies.ServiceProvider.GetRequiredService<ITableRepository>();
    }
    public void Dispose()
    {
        _database.Clear();
    }
    
    [Fact]
    public async Task ShouldCreateTable()
    {
        var capacity = 4;
        var command = new CreateTableCommand(capacity);
        var id = await new CreateTableCommandHandler(_tableRepository).Handle(command, CancellationToken.None);
        var table = await _tableRepository.GetById(id);
        Assert.Equal(capacity, table.Capacity);
    }
    
    [Fact]
    public async Task ShouldNotCreateTableWithNegativeCapacity()
    {
        var capacity = -1;
        var command = new CreateTableCommand(capacity);
        await Assert.ThrowsAsync<InvalidTableCapacity>(() => new CreateTableCommandHandler(_tableRepository).Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ShouldNotCreateTableWithZeroCapacity()
    {
        var capacity = 0;
        var command = new CreateTableCommand(capacity);
        await Assert.ThrowsAsync<InvalidTableCapacity>(() => new CreateTableCommandHandler(_tableRepository).Handle(command, CancellationToken.None));
    }
    
    [Fact]
    public async Task AdminShouldBeAbleToCreateTableWithSpecifiedCapacity()
    {
        var capacity = 5;
        var command = new CreateTableCommand(capacity);
        var handler = new CreateTableCommandHandler(_tableRepository);

        var id = await handler.Handle(command, CancellationToken.None);

        var table = await _tableRepository.GetById(id);
        Assert.Equal(capacity, table.Capacity);
    }
}