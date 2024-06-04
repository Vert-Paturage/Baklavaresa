using Application.Reservation.Commands.CreateReservation;
using Application.Services;
using Domain.Exceptions.Table;
using Domain.Repositories;
using Infrastructure.Data.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Crypto.Signers;

namespace Application.UseCasesTests.Reservation;

public class TableChoice: IClassFixture<Dependencies>, IDisposable
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IClockService _clockService;
    private readonly IMediator _mediator;
    private readonly IDatabase _database;
    
    private string _firstName;
    private string _lastName;
    private string _email;
    private DateTime _date;
    
    public TableChoice(Dependencies dependencies)
    {
        _database = dependencies.ServiceProvider.GetRequiredService<IDatabase>();
        _reservationRepository = dependencies.ServiceProvider.GetRequiredService<IReservationRepository>();
        _mediator = dependencies.ServiceProvider.GetRequiredService<IMediator>();
        _clockService = dependencies.ServiceProvider.GetRequiredService<IClockService>();
        _tableRepository = dependencies.ServiceProvider.GetRequiredService<ITableRepository>();
        
        _firstName = "John";
        _lastName = "Doe";
        _email = "john.doe@lecnam.net";
        _date = _clockService.Now.AddDays(1);
    }
    
    public void Dispose()
    {
        _database.Clear();
    }

    [Fact]
    public async Task TableChoice_WithEqualCapacity_ShouldAssignTable()
    {
        var goodTable = await _tableRepository.Create(new Domain.Entities.Table(2));
        await _tableRepository.Create(new Domain.Entities.Table(4));   
        await _tableRepository.Create(new Domain.Entities.Table(6));
        await _tableRepository.Create(new Domain.Entities.Table(8));
        
        int numberOfPeople = 2;
            
        var command = new CreateReservationCommand(_firstName, _lastName, _email, _date, numberOfPeople);
        var id = await _mediator.Send(command);
        var reservation = await _reservationRepository.GetById(id);
            
        Assert.Equal(goodTable, reservation.Table.Id);
    }

    [Fact]
    public async Task TableChoice_WithGreaterCapacity_ShouldAssignTable()
    {
        var good_table = await _tableRepository.Create(new Domain.Entities.Table(4));
        await _tableRepository.Create(new Domain.Entities.Table(2));
        await _tableRepository.Create(new Domain.Entities.Table(6));
        await _tableRepository.Create(new Domain.Entities.Table(8));

        int numberOfPeople = 2;
        
        var command = new CreateReservationCommand(_firstName, _lastName, _email, _date, numberOfPeople);
        var id = await _mediator.Send(command);
        var reservation = await _reservationRepository.GetById(id);
        
        Assert.Equal(good_table, reservation.Table.Id);
    }
    
    [Fact]
    public async Task TableChoice_WithSmallerCapacity_ShouldNotAssignTable()
    {
        await _tableRepository.Create(new Domain.Entities.Table(2));
        await _tableRepository.Create(new Domain.Entities.Table(4));
        await _tableRepository.Create(new Domain.Entities.Table(6));
        await _tableRepository.Create(new Domain.Entities.Table(8));
        
        int numberOfPeople = 10;
        
        var command = new CreateReservationCommand(_firstName, _lastName, _email, _date, numberOfPeople);
        
        await Assert.ThrowsAsync<NoTablesAvailableException>(async () => await _mediator.Send(command));
    }
}