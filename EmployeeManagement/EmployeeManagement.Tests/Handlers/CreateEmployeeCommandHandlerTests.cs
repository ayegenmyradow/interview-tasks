using EmployeeManagement.Core.CQRS.Commands;
using EmployeeManagement.Core.Models;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.Handlers.Commands;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EmployeeManagement.Tests.Handlers
{
    public class CreateEmployeeCommandHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly CreateEmployeeCommandHandler _handler;

        public CreateEmployeeCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
                .Options;

            _context = new ApplicationDbContext(options);
            _handler = new CreateEmployeeCommandHandler(_context);

            // Seed test data
            var position = new Position 
            { 
                Id = 1, 
                Title = "Developer", 
                BaseSalary = 50000m,
                Description = "Software Developer"
            };
            _context.Positions.Add(position);
            _context.SaveChanges();
        }

        [Fact]
        public async Task Handle_CreatesEmployee_WhenValidData()
        {
            // Arrange
            var command = new CreateEmployeeCommand
            {
                FirstName = "John",
                LastName = "Doe",
                MiddleName = "Robert",
                DateOfBirth = DateTime.UtcNow.AddYears(-30),
                HireDate = DateTime.UtcNow,
                PositionId = 1
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
            Assert.Equal(command.FirstName, result.FirstName);
            Assert.Equal(command.LastName, result.LastName);
            Assert.Equal(command.MiddleName, result.MiddleName);
            Assert.Equal(command.PositionId, result.PositionId);
            
            // Verify position details
            Assert.NotNull(result.Position);
            Assert.Equal("Developer", result.Position.Title);
            Assert.Equal(50000m, result.Position.BaseSalary);
            Assert.Equal("Software Developer", result.Position.Description);

            // Verify database state
            var savedEmployee = await _context.Employees.FindAsync(result.Id);
            Assert.NotNull(savedEmployee);
            Assert.Equal(command.FirstName, savedEmployee.FirstName);
            Assert.Equal(command.LastName, savedEmployee.LastName);
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenPositionNotFound()
        {
            // Arrange
            var command = new CreateEmployeeCommand
            {
                FirstName = "John",
                LastName = "Doe",
                MiddleName = "Robert",
                DateOfBirth = DateTime.UtcNow.AddYears(-30),
                HireDate = DateTime.UtcNow,
                PositionId = 999 // Non-existent position
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _handler.Handle(command, CancellationToken.None));
        }
    }
} 