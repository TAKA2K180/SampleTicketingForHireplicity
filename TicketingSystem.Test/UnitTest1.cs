using Microsoft.EntityFrameworkCore;
using System;
using TicketingSystem.Core.Enums;
using TicketingSystem.Core.Models;
using TicketingSystem.Data.Contracts;
using TicketingSystem.Data.Implementation;
using TicketingSystem.Data.Repositories;

namespace TicketingSystem.Test
{
    public class Tests
    {
        private ApplicationDbContext _dbContext;
        private ITicketRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _dbContext = new ApplicationDbContext(options);
            _repository = new TicketRepository(_dbContext);
        }

        [Test]
        public async Task AddAsync_ShouldAddTicket()
        {
            // Arrange
            var ticket = new Tickets
            {
                Id = Guid.NewGuid(),
                BuildingCode = "BLDG001",
                Description = "No internet connection",
                CreatedBy = "Roi",
                CreatedDate = DateTime.UtcNow,
                LastModifiedBy = "Roi",
                CurrentStatus = CurrentStatus.Created
            };

            // Act
            await _repository.AddAsync(ticket);
            await _repository.SaveAsync();
            var allTickets = await _repository.GetAllAsync();

            // Assert
            Assert.AreEqual(1, allTickets.Count());
            Assert.AreEqual("BLDG001", allTickets.First().BuildingCode);
        }
    }
}