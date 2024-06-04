using System.Runtime.CompilerServices;
using Application.Reservation.Commands.DeleteReservation;
using Application.Services;
using Domain.Repositories;
using Infrastructure.Data.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCasesTests.Admin;

public class DeleteReservation: IClassFixture<Dependencies>
{
    private readonly IReservationRepository _reservationRepository;
       private readonly IClockService _clockService;
       private readonly IMediator _mediator;
       private readonly IDatabase _database;
       public DeleteReservation(Dependencies dependencies)
       {
           _database = dependencies.ServiceProvider.GetRequiredService<IDatabase>();
           _reservationRepository = dependencies.ServiceProvider.GetRequiredService<IReservationRepository>();
           _mediator = dependencies.ServiceProvider.GetRequiredService<IMediator>();
           _clockService = dependencies.ServiceProvider.GetRequiredService<IClockService>();
           
           // Seed tables
           var tableRepository = dependencies.ServiceProvider.GetRequiredService<ITableRepository>();
           var idTable1 = tableRepository.Create(new Domain.Entities.Table(2)).Result;
           var idTable2 = tableRepository.Create(new Domain.Entities.Table(2)).Result;
           var idTable3 = tableRepository.Create(new Domain.Entities.Table(4)).Result;
           var idTable4 = tableRepository.Create(new Domain.Entities.Table(4)).Result;
           
           var table1 = tableRepository.GetById(idTable1);
           var table2 = tableRepository.GetById(idTable2);
           var table3 = tableRepository.GetById(idTable3);
           var table4 = tableRepository.GetById(idTable4);
              
           // Seed reservations
           _reservationRepository.Create(new Domain.Entities.Reservation("John", "Doe", "john.doe@lecnam.net", _clockService.Now, 2, table1)).Wait();
           _reservationRepository.Create(new Domain.Entities.Reservation("Jane", "Doe", "jane.doe@lecnam.net", _clockService.Now.AddHours(1), 2, table2)).Wait();
           _reservationRepository.Create(new Domain.Entities.Reservation("Bob", "Smith", "bob.smith@lecnam.net", _clockService.Now.AddHours(2), 4, table3)).Wait();
           _reservationRepository.Create(new Domain.Entities.Reservation("Alice", "Johnson", "alice.johnson@lecnam.net", _clockService.Now.AddHours(3), 4, table4)).Wait();
           
           //id =_reservationRepository.Create(new Domain.Entities.Reservation("Bob2", "Smith2", "bob.smith2@lecnam.net", _clockService.Now.AddHours(12), 2, table1)).Result;
       }
       [Fact]
       public async Task ShouldDeleteReservation()
       {
              var reservations = await _reservationRepository.GetReservationsByDate(_clockService.CurrentMonth);
              var reservation = reservations.First();
              var id = reservation.Id;
              await _mediator.Send(new DeleteReservationCommand(id));
              await Assert.ThrowsAsync<Domain.Exceptions.Reservation.ReservationNotFoundException>(async () => await _reservationRepository.GetById(id));
       }
       [Fact]
       public async Task ShouldThrowExceptionWhenReservationDoesNotExist()
       {
           // get last given id
           var reservations = await _reservationRepository.GetReservationsByDate(_clockService.CurrentMonth);
           var reservation = reservations.Last();
           var nonExistentId = reservation.Id + 1;
           await Assert.ThrowsAsync<Domain.Exceptions.Reservation.ReservationNotFoundException>(async () => await _mediator.Send(new DeleteReservationCommand(nonExistentId)));
       }
       [Fact]
       public async Task ShouldThrowExceptionWhenReservationAlreadyDeleted()
       {
           var reservations = await _reservationRepository.GetReservationsByDate(_clockService.CurrentMonth);
           var reservation = reservations.First();
           var id = reservation.Id;
           await _mediator.Send(new DeleteReservationCommand(id));
           await Assert.ThrowsAsync<Domain.Exceptions.Reservation.ReservationNotFoundException>(async () => await _mediator.Send(new DeleteReservationCommand(id)));
       }
}
