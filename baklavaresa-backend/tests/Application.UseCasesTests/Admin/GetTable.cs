using Application.Reservation.Queries.GetAllTables;
using Domain.Repositories;
using Infrastructure.Data.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Crypto.Signers;

namespace Application.UseCasesTests.Admin;

public class GetTable: IClassFixture<Dependencies>, IDisposable
{
    private readonly IDatabase _database;
    private readonly ITableRepository _tableRepository;
    private readonly IMediator _mediator;
    public GetTable(Dependencies dependencies)
    {
        _database = dependencies.ServiceProvider.GetRequiredService<IDatabase>();
        _tableRepository = dependencies.ServiceProvider.GetRequiredService<ITableRepository>();
        _mediator = dependencies.ServiceProvider.GetRequiredService<IMediator>();
    }    
    public void Dispose()
    {
        _database.Clear();
    }
    
    [Fact]
    public async Task ShouldGetTable()
    {
        var tableCapacity = new List<int>()
        {
            2, 2, 4, 4, 10,  20, 500, 5654
        };
        foreach (var capacity in tableCapacity)
        {
            await _tableRepository.Create(new Domain.Entities.Table(capacity));
        }
        var table = await _mediator.Send(new GetAllTablesQuery());
        Assert.Equal(tableCapacity.Count, table.Count);
        foreach (var capacity in tableCapacity)
        {
            Assert.Contains(table, x => x.Capacity == capacity);
        }
    }
    
    [Fact]
    public async Task ShouldGetTableWithSpecifiedCapacity()
    {
        var table = await _mediator.Send(new GetAllTablesQuery());
        Assert.Empty(table);
    }
}