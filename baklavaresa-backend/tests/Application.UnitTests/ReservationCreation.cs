using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UnitTests;

public class ReservationCreation: IClassFixture<Dependencies>
{
   private readonly IReservationRepository _reservationRepository;
   
   public ReservationCreation(Dependencies dependencies)
   {
       _reservationRepository = dependencies.ServiceProvider.GetRequiredService<IReservationRepository>();
   }

   public void Dispose()
   {
   }
}