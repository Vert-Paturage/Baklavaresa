using System.Runtime.InteropServices.JavaScript;
using Application.Reservation.Queries.GetAvailableSlots;
using Application.Services;
using Domain.Exceptions.Reservation;
using Domain.Repositories;
using Infrastructure.Data.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCasesTests.Reservation;

public class GetAvailableSlots: IClassFixture<Dependencies>, IDisposable
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IClockService _clockService;
    private readonly IMediator _mediator;
    private readonly IDatabase _database;
    
    private string _firstName;
    private string _lastName;
    private string _email;
       
    public GetAvailableSlots(Dependencies dependencies)
    {
        _database = dependencies.ServiceProvider.GetRequiredService<IDatabase>();
        _reservationRepository = dependencies.ServiceProvider.GetRequiredService<IReservationRepository>();
        _tableRepository = dependencies.ServiceProvider.GetRequiredService<ITableRepository>();
        _mediator = dependencies.ServiceProvider.GetRequiredService<IMediator>();
        _clockService = dependencies.ServiceProvider.GetRequiredService<IClockService>();
        
        _firstName = "John";
        _lastName = "Doe";
        _email = "john.doe@gmail.com";
    }
    
    public void Dispose()
    {
        _database.Clear();
    }

    [Fact]
    public async Task GetAvailableSlots_WithValidData_ShouldReturnAvailableSlots()
    {
        await _tableRepository.Create(new Domain.Entities.Table(2));
        var goodTable = await _tableRepository.Create(new Domain.Entities.Table(2));
        await _tableRepository.Create(new Domain.Entities.Table(4));
        var date = _clockService.Now.AddDays(1);
                
        var reservation = new Domain.Entities.Reservation(_firstName, _lastName, _email, date, 2, _tableRepository.GetAll().Result[0]);
        _reservationRepository.Create(reservation);
        reservation = new Domain.Entities.Reservation(_firstName, _lastName, _email, date, 2, _tableRepository.GetAll().Result[1]);
        _reservationRepository.Create(reservation);
        
        var query = new GetAvailableSlotsQuery(2, date);
        var availableSlots = await _mediator.Send(query);

        Assert.NotNull(availableSlots);
        Assert.Equal(DateTime.DaysInMonth(date.Year, date.Month), availableSlots.Count);
        var availableSlot = (availableSlots.Where(a => a.Day == date.GetBakDay().ToDateTime()));
        Assert.Equal(1,availableSlot.Count());
        Assert.Single(availableSlot);
        
    }
    
    [Fact]
    public async Task GetAvailableSlots_WithInvalidNumberOfPeople_ShouldThrowInvalidNumberOfPeopleException()
    {
        var date = _clockService.Now.AddDays(1);
        var query = new GetAvailableSlotsQuery(0, date);
        await Assert.ThrowsAsync<InvalidNumberOfPeopleException>(() => _mediator.Send(query));
    }
    
    [Fact]
    public async Task GetAvailableSlots_WithNoAvailableTablesForNumberOfPeople_ShouldReturnEmptySlots()
    {
        await _tableRepository.Create(new Domain.Entities.Table(2));
        await _tableRepository.Create(new Domain.Entities.Table(2));
        await _tableRepository.Create(new Domain.Entities.Table(2));
        var date = _clockService.Now.AddDays(1);
                
        var reservation = new Domain.Entities.Reservation(_firstName, _lastName, _email, date, 2, _tableRepository.GetAll().Result[0]);
        _reservationRepository.Create(reservation);
        reservation = new Domain.Entities.Reservation(_firstName, _lastName, _email, date, 2, _tableRepository.GetAll().Result[1]);
        _reservationRepository.Create(reservation);
        
        var query = new GetAvailableSlotsQuery(4, date);
        var availableSlots = await _mediator.Send(query);

        Assert.NotNull(availableSlots);
        Assert.Equal(DateTime.DaysInMonth(date.Year, date.Month), availableSlots.Count);
        var availableSlot = (availableSlots.Where(a => a.Day == date.GetBakDay().ToDateTime()));
        Assert.Equal(1,availableSlot.Count());
        Assert.Single(availableSlot);
        Assert.Empty(availableSlot.First().Slots);
    }
    
    [Fact]
    public async Task GetAvailableSlots_WithNoAvailableTables_ShouldReturnEmptySlots()
    {
        var date = _clockService.Now.AddDays(1);
                
        var query = new GetAvailableSlotsQuery(2, date);
        var availableSlots = await _mediator.Send(query);

        Assert.NotNull(availableSlots);
        Assert.Equal(DateTime.DaysInMonth(date.Year, date.Month), availableSlots.Count);
        var availableSlot = (availableSlots.Where(a => a.Day == date.GetBakDay().ToDateTime()));
        Assert.Equal(1,availableSlot.Count());
        Assert.Single(availableSlot);
        Assert.Empty(availableSlot.First().Slots);
    }
    
    [Fact]
    public async Task GetAvailableSlots_WithPassedSlots_ShouldReturnEmptySlots()
    {
        await _tableRepository.Create(new Domain.Entities.Table(2));
                
        var query = new GetAvailableSlotsQuery(2, _clockService.CurrentMonth);
        var availableSlots = await _mediator.Send(query);

        var passedSlots = availableSlots.Where(a => a.Day <=  (DateTime)_clockService.Now.GetBakDay());
        foreach (var slot in passedSlots)
        {
            if (slot.Day == (DateTime)_clockService.Now.GetBakDay())
            {
                Assert.NotNull(slot.Slots);
                var passedSlot = slot.Slots.Where(s => s < _clockService.Now.ToDateTime());
                Assert.Empty(passedSlot); // Check if there are no slots in the past
                continue;
            }
            Assert.NotNull(slot.Slots);
            Assert.Empty(slot.Slots);
        }
    }
}