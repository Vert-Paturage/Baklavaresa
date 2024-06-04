using Application.Reservation.Commands.CreateReservation;
using Application.Reservation.Queries.GetAllReservations;
using Application.Reservation.Queries.GetAllTables;
using Application.Services;
using Domain.Dates;
using Domain.Repositories;
using Infrastructure.Data.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCasesTests.Admin;

internal record ReservationTuple(int NumberOfPeople, BakDate Date);

public class GetReservation : IClassFixture<Dependencies>, IDisposable
{
    private readonly IDatabase _database;
    private readonly ITableRepository _tableRepository;
    private readonly IMediator _mediator;
    private readonly IClockService _clockService;
    
    private string _firstName = "John";
    private string _lastName = "Doe";
    private string _email = "john.doe@lecnam.net";

    public GetReservation(Dependencies dependencies)
    {
        _database = dependencies.ServiceProvider.GetRequiredService<IDatabase>();
        _tableRepository = dependencies.ServiceProvider.GetRequiredService<ITableRepository>();
        _mediator = dependencies.ServiceProvider.GetRequiredService<IMediator>();
        _clockService = dependencies.ServiceProvider.GetRequiredService<IClockService>();
        
        // Seed tables
        _tableRepository.Create(new Domain.Entities.Table(6));
        _tableRepository.Create(new Domain.Entities.Table(6));
    }

    public void Dispose()
    {
        _database.Clear();
    }
    
    [Fact]
    public async Task ShouldGetReservation()
    {
        var reservationTuples = new List<ReservationTuple>()
        {
            new ReservationTuple(1, _clockService.Now.AddDays(1)),
            new ReservationTuple(2, _clockService.Now.AddDays(1).AddHours(2)),
            new ReservationTuple(3, _clockService.Now.AddDays(30)),
        };
        
        foreach (var reservationTuple in reservationTuples)
        {
            var command = new CreateReservationCommand(_firstName, _lastName, _email, reservationTuple.Date, reservationTuple.NumberOfPeople);
            await _mediator.Send(command);
        }
        
        var reservations = await _mediator.Send(new GetAllReservationsQuery(_clockService.Now.AddDays(1)));
        Assert.Equal(2, reservations.Count);
        
        reservations = await _mediator.Send(new GetAllReservationsQuery(_clockService.Now.AddDays(30)));
        Assert.Single(reservations);
    }
}