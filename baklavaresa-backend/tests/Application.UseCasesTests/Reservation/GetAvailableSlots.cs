using Application.Reservation.Queries.GetAvailableSlots;
using Application.Services;
using Domain.Repositories;
using Infrastructure.Data.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCasesTests.Reservation;

public class GetAvailableSlots: IClassFixture<Dependencies>, IDisposable
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IClockService _clockService;
    private readonly IMediator _mediator;
    private readonly IDatabase _database;
    
    private string _firstName;
    private string _lastName;
    private string _email;
    private DateTime _date;
       
    public GetAvailableSlots(Dependencies dependencies)
    {
        _database = dependencies.ServiceProvider.GetRequiredService<IDatabase>();
        _reservationRepository = dependencies.ServiceProvider.GetRequiredService<IReservationRepository>();
        _mediator = dependencies.ServiceProvider.GetRequiredService<IMediator>();
        _clockService = dependencies.ServiceProvider.GetRequiredService<IClockService>();
       
        // Seed tables
        var tableRepository = dependencies.ServiceProvider.GetRequiredService<ITableRepository>();
        tableRepository.Create(new Domain.Entities.Table(2));
        tableRepository.Create(new Domain.Entities.Table(2));      
        tableRepository.Create(new Domain.Entities.Table(4));      
        tableRepository.Create(new Domain.Entities.Table(4));      
        
    }
    
    public void Dispose()
    {
        _database.Clear();
    }

    [Fact]
    public async Task GetAvailableSlots_WithValidData_ShouldReturnAvailableSlots()
    {
        var numberOfPeople = 2;
        var month = _clockService.Now;
        
        var availableSlots = await _mediator.Send(new GetAvailableSlotsQuery(numberOfPeople, month));
        
        
    }
}