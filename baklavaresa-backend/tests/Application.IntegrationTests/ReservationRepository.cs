using Domain.Entities;
using Domain.Exceptions.Reservation;
using Domain.Repositories;
using Infrastructure.Data.Persistence;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace Integration
{
    public class ReservationRepositoryTests : IClassFixture<SetupDependencies>, IDisposable
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IDatabase _database;

        public ReservationRepositoryTests(SetupDependencies fixture)
        {
            _reservationRepository = fixture.ServiceProvider.GetService<IReservationRepository>();
            _database = fixture.ServiceProvider.GetService<IDatabase>();
            _tableRepository = fixture.ServiceProvider.GetService<ITableRepository>();
        }

        public void Dispose()
        {
            _database.Clear();
        }

        [Fact]
        public async Task CreateReservationWithIdTest()
        {
            Reservation reservation = new Reservation
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "odede@ladetail.fr",
                Date = System.DateTime.Now,
                NumberOfPeople = 2,
                Table = new Table(4)
            };
            var reservationId = await _reservationRepository.Create(reservation);
            Assert.That(reservationId, Is.Not.EqualTo(default(int)));
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
                Table = new Table(4)
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
                Table = new Table(4)
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
                Table = new Table(4)
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
                Table = new Table(4)
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
                Table = new Table(4)
            };
            var reservationId = _reservationRepository.Create(reservation).Result;
            _reservationRepository.Delete(reservationId);
            Assert.ThrowsAsync<ReservationNotFoundException>(() => _reservationRepository.GetReservationById(reservationId));
        }

    }
      
}
