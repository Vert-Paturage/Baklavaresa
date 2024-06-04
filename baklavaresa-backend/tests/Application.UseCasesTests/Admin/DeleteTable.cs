using Application.Table.Commands.CreateTable;
using Application.Table.Commands.DeleteTable;
using Domain.Exceptions.Table;
using Domain.Repositories;
using Infrastructure.Data.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCasesTests.Admin
{
    public class DeleteTableTests : IClassFixture<Dependencies>, IDisposable
    {
        private readonly IDatabase _database;
        private readonly ITableRepository _tableRepository;
        private readonly IMediator _mediator;
        
        public void Dispose()
        {
            _database.Clear();
        }

        public DeleteTableTests(Dependencies dependencies)
        {
            _database = dependencies.ServiceProvider.GetRequiredService<IDatabase>();
            _tableRepository = dependencies.ServiceProvider.GetRequiredService<ITableRepository>();
            _mediator = dependencies.ServiceProvider.GetRequiredService<IMediator>();
        }
        
        [Fact]
        public async Task ShouldDeleteTable()
        {
            var tableId = await _tableRepository.Create(new Domain.Entities.Table(4));
            var command = new DeleteTableCommand(tableId);
            await _mediator.Send(command);
            var tables = await _tableRepository.GetAll();
            Assert.Empty(tables);
        }
        
        [Fact]
        public async Task ShouldNotDeleteTableWithInvalidId()
        {
            var invalidId = -1;
            var command = new DeleteTableCommand(invalidId);
            await Assert.ThrowsAsync<TableNotFoundException>(() => _mediator.Send(command));
        }
    }
}