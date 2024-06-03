using System.Runtime.CompilerServices;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.UseCases;

public class ReservationCreation: IClassFixture<Dependencies>
{
   private readonly IReservationRepository _reservationRepository;
   
   public ReservationCreation(Dependencies dependencies)
   {
       _reservationRepository = dependencies.ServiceProvider.GetRequiredService<IReservationRepository>();
   }

   [Fact]
   public void ShouldCreateReservation()
   {
      Assert.Fail(); 
   }
}
