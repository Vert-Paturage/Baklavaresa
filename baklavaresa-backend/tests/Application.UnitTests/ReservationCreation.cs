using Domain.Repositories;

namespace Application.UnitTests;

public class ReservationCreation: IClassFixture<Dependencies>
{
   private readonly IReservationRepository _reservationRepository;
   
   public ReservationCreation(Dependencies dependencies)
   {
       _reservationRepository = dependencies.;
   }
}