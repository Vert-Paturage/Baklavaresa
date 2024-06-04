using Domain.Entities;
using Domain.Exceptions.Table;
using Domain.Repositories;
using Infrastructure.Data.Persistence;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace Integration
{
    public class TableRepositoryTests : IClassFixture<SetupDependencies>, IDisposable
    {
        private readonly ITableRepository _tableRepository;
        private readonly IDatabase _database;

        public TableRepositoryTests(SetupDependencies fixture)
        {
            _tableRepository = fixture.ServiceProvider.GetService<ITableRepository>();
            _database = fixture.ServiceProvider.GetService<IDatabase>();
        }

        public void Dispose()
        {
            _database.Clear();
        }

        [Fact]
        public void TableCreatWithIdTest()
        {
            // Arrange
            var table = new Table
            {
                Capacity = 4,
            };

            // Act
            var tableId = _tableRepository.Create(table).Result;

            // Assert
            Assert.That(tableId, Is.Not.EqualTo(default(int)));
        }

        [Fact]
        public async void TableGetByIdTest()
        {
            // Arrange
            var table = new Table
            {
                Capacity = 4,
            };

            var tableId = _tableRepository.Create(table).Result;

            // Act
            var tableFromDb = await _tableRepository.GetById(tableId);

            // Assert
            Assert.That(tableFromDb, Is.Not.Null);
            Assert.That(tableFromDb.Capacity, Is.EqualTo(4));
        }

        [Fact]
        public void TableGetAllTest()
        {
            // Arrange
            var table1 = new Table
            {
                Capacity = 4,
            };

            var table2 = new Table
            {
                Capacity = 2,
            };

            _tableRepository.Create(table1);
            _tableRepository.Create(table2);

            // Act
            var tables = _tableRepository.GetAll().Result;

            // Assert
            Assert.That(tables, Is.Not.Null);
            Assert.That(tables.Count, Is.EqualTo(2));
        }
        [Fact]
        public void TableGetTablesByCapacityTest()
        {
            // Arrange
            var table1 = new Table
            {
                Capacity = 4,
            };

            var table2 = new Table
            {
                Capacity = 2,
            };

            _tableRepository.Create(table1);
            _tableRepository.Create(table2);

            // Act
            var tables = _tableRepository.GetTablesByCapacity(4).Result;

            // Assert
            Assert.That(tables, Is.Not.Null);
            Assert.That(tables.Count, Is.EqualTo(1));
            Assert.That(tables[0].Capacity, Is.EqualTo(4));
        }
        [Fact]
        public void TableGetAvailableTableTest()
        {
            // Arrange
            var table1 = new Table
            {
                Capacity = 4,
            };

            var table2 = new Table
            {
                Capacity = 2,
            };

            _tableRepository.Create(table1);
            _tableRepository.Create(table2);

            // Act
            var tables = _tableRepository.GetAvailableTable(DateTime.Now, 2).Result;

            // Assert
            Assert.That(tables, Is.Not.Null);
            Assert.That(tables.Count, Is.EqualTo(2));
        }
        [Fact]
        public void TableDeleteTest()
        {
            // Arrange
            var table = new Table
            {
                Capacity = 4,
            };

            var tableId = _tableRepository.Create(table).Result;

            // Act
            _tableRepository.Delete(tableId).Wait();

            // Assert
            Assert.Throws<TableNotFoundException>(()=>_tableRepository.GetById(tableId));
        }
        
    }
}
