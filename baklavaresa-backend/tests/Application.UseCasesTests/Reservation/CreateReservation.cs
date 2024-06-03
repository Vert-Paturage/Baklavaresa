using Application.Reservation.Commands.CreateReservation;
using Domain.Repositories;

namespace Application.UseCasesTests.Reservation;

public class CreateReservation
{
   private readonly IReservationRepository _reservationRepository;
   public CreateReservation(IReservationRepository reservationRepository)
   {
       _reservationRepository = reservationRepository;
   }

   [Fact]
   public void ShouldCreateReservation()
   {
   }
}