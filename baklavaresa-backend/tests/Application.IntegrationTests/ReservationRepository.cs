using Domain.Entities;
using Domain.Exceptions.Reservation;
using Domain.Repositories;
using Infrastructure.Data.Persistence;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Integration
{
    public class ReservationRepositoryTests : IClassFixture<SetupDependencies>, IDisposable
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IDatabase _database;

        public ReservationRepositoryTests(SetupDependencies fixture)
        {
            _reservationRepository = fixture.ServiceProvider.GetService<IReservationRepository>();
            _database = fixture.ServiceProvider.GetService<IDatabase>();
        }

        public void Dispose()
        {
            _database.Clear();
        }

        [Fact]
        public void  CreateReservationWithIdTest()
        {
            Reservation reservation = new Reservation
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "odede@ladetail.fr",
                Date = System.DateTime.Now,
                NumberOfPeople = 2,

            };
            var reservationId = _reservationRepository.Create(reservation).Result;
            Assert.That(reservationId, Is.Not.EqualTo(default(int)));
        }
        [Fact]
        public void GetReservationByIdTest()
        {
            Reservation reservation = new Reservation
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "odede@ladetail.fr",
                Date = System.DateTime.Now,
                NumberOfPeople = 2,

            };
            var reservationId = _reservationRepository.Create(reservation).Result;
            var reservationFromDb = _reservationRepository.GetReservationById(reservationId).Result;
            Assert.That(reservationFromDb, Is.Not.Null);
        }
        [Fact]
        public void GetReservationByMonthTest()
        {
            Reservation reservation = new Reservation
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "odede@ladetail.fr",
                Date = System.DateTime.Now,
                NumberOfPeople = 2,

            };
            _reservationRepository.Create(reservation);
            var reservationFromDb = _reservationRepository.GetReservationByMonth(System.DateTime.Now).Result;
            Assert.That(reservationFromDb, Is.Not.Null);
        }
        [Fact]
        public void GetReservationsBySlotTest()
        {
            Reservation reservation = new Reservation
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "odede@ladetail.fr",
                Date = System.DateTime.Now,
                NumberOfPeople = 2,

            };
            _reservationRepository.Create(reservation);
            var reservationFromDb = _reservationRepository.GetReservationsBySlot(System.DateTime.Now, System.DateTime.Now.AddHours(1)).Result;
            Assert.That(reservationFromDb, Is.Not.Null);
        }
        [Fact]
        public void GetReservationsByDateTest()
        {
            Reservation reservation = new Reservation
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "odede@ladetail.fr",
                Date = System.DateTime.Now,
                NumberOfPeople = 2,

            };
            _reservationRepository.Create(reservation);
            var reservationFromDb = _reservationRepository.GetReservationsByDate(System.DateTime.Now).Result;
            Assert.That(reservationFromDb, Is.Not.Null);
        }
        [Fact]
        public void GetReservationByTableIdTest()
        {
            Reservation reservation = new Reservation
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "odede@ladetail.fr",
                Date = System.DateTime.Now,
                NumberOfPeople = 2,

            };
            _reservationRepository.Create(reservation);
            var reservationFromDb = _reservationRepository.GetReservationByTableId(1).Result;
            Assert.That(reservationFromDb, Is.Not.Null);
        }
        [Fact]
        public void DeleteReservationTest()
        {
            Reservation reservation = new Reservation
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "odede@ladetail.fr",
                Date = System.DateTime.Now,
                NumberOfPeople = 2,

            };
            var reservationId = _reservationRepository.Create(reservation).Result;
            _reservationRepository.Delete(reservationId);
            Assert.ThrowsAsync<ReservationNotFoundException>(() => _reservationRepository.GetReservationById(reservationId));
        }

    }
      
}
