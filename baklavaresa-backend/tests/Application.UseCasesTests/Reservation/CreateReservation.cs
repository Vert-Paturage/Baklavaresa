using System.Runtime.InteropServices.JavaScript;
using Application.Reservation.Commands.CreateReservation;
using Application.Services;
using Application.UseCasesTests.Fakes;
using Domain.Exceptions.Reservation;
using Domain.Exceptions.Table;
using Domain.Repositories;
using Infrastructure.Data.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCasesTests.Reservation;

public class CreateReservation: IClassFixture<Dependencies>, IDisposable
{
   private readonly IReservationRepository _reservationRepository;
   private readonly IClockService _clockService;
   private readonly IMediator _mediator;
   private readonly IDatabase _database;
   public CreateReservation(Dependencies dependencies)
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
   public async Task ShouldCreateReservation()
   {
    var firstName = "John";
    var lastName = "Doe";
    var email = "john.doe@lecnam.net";
    var date = _clockService.Now.AddDays(1); 
    int numberOfPeople = 2;
    
    var command = new CreateReservationCommand(firstName, lastName, email, date, numberOfPeople);
    var id = await _mediator.Send(command);
    var reservation = await _reservationRepository.GetById(id);
    
    // Assert
    Assert.Equal(firstName, reservation.FirstName);
    Assert.Equal(lastName, reservation.LastName);
    Assert.Equal(email, reservation.Email);
    Assert.Equal(date, reservation.Date);
    Assert.Equal(numberOfPeople, reservation.NumberOfPeople);
    Assert.NotNull(reservation.Table);
   }
   
   [Fact]
   public async Task ShouldNotCreateReservation_WhenNumberOfPeopleExceedsCapacity()
   {
       var firstName = "John";
       var lastName = "Doe";
       var email = "john.doe@lecnam.net";
       var date = _clockService.Now.AddDays(1);
       int numberOfPeople = 10; // Exceeds the capacity of the tables

       var command = new CreateReservationCommand(firstName, lastName, email, date, numberOfPeople);
    
       await Assert.ThrowsAsync<NoTablesAvailableException>(() => _mediator.Send(command));
   }

   [Fact]
   public async Task ShouldNotCreateReservation_WhenDateIsInThePast()
   {
       var firstName = "John";
       var lastName = "Doe";
       var email = "john.doe@lecnam.net";
       var date = _clockService.Now.AddDays(-1); // Date is in the past
       int numberOfPeople = 2;

       var command = new CreateReservationCommand(firstName, lastName, email, date, numberOfPeople);
    
       await Assert.ThrowsAsync<InvalidReservationDateException>(() => _mediator.Send(command));
   }

   [Fact]
   public async Task ShouldNotCreateReservation_WhenEmailIsInvalid()
   {
       var firstName = "John";
       var lastName = "Doe";
       var email = "john.doe"; // Invalid email
       var date = _clockService.Now.AddDays(1);
       int numberOfPeople = 2;

       var command = new CreateReservationCommand(firstName, lastName, email, date, numberOfPeople);
    
       await Assert.ThrowsAsync<InvalidEmailException>(() => _mediator.Send(command));
   }

   [Fact]
   public async Task ShouldNotCreateReservation_WhenFirstNameOrLastNameIsEmpty()
   {
       var firstName = ""; // Empty first name
       var lastName = "Doe";
       var email = "john.doe@lecnam.net";
       var date = _clockService.Now.AddDays(1);
       int numberOfPeople = 2;

       var command = new CreateReservationCommand(firstName, lastName, email, date, numberOfPeople);
    
       await Assert.ThrowsAsync<InvalidNameException>(() => _mediator.Send(command));
   } 
   
   [Fact]
   public async Task ShouldNotCreateReservation_WhenNumberOfPeopleIsZeroOrNegative()
   {
       var firstName = "John";
       var lastName = "Doe";
       var email = "john.doe@lecnam.net";
       var date = _clockService.Now.AddDays(1);
       int numberOfPeople = 0; // Zero people

       var command = new CreateReservationCommand(firstName, lastName, email, date, numberOfPeople);

       await Assert.ThrowsAsync<Domain.Exceptions.Reservation.InvalidNumberOfPeopleException>(() => _mediator.Send(command));
   }

   [Fact]
   public async Task ShouldNotCreateReservation_WhenEmailIsNull()
   {
       var firstName = "John";
       var lastName = "Doe";
       string email = null; // Null email
       var date = _clockService.Now.AddDays(1);
       int numberOfPeople = 2;

       var command = new CreateReservationCommand(firstName, lastName, email, date, numberOfPeople);

       await Assert.ThrowsAsync<Domain.Exceptions.Reservation.InvalidEmailException>(() => _mediator.Send(command));
   }

   [Fact]
   public async Task ShouldNotCreateReservation_WhenFirstNameOrLastNameIsNull()
   {
       string firstName = null; // Null first name
       var lastName = "Doe";
       var email = "john.doe@lecnam.net";
       var date = _clockService.Now.AddDays(1);
       int numberOfPeople = 2;

       var command = new CreateReservationCommand(firstName, lastName, email, date, numberOfPeople);

       await Assert.ThrowsAsync<Domain.Exceptions.Reservation.InvalidNameException>(() => _mediator.Send(command));
   }
}