using EmployeeManagement.Core.CQRS.Queries;
using EmployeeManagement.Core.Models;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.Handlers.Queries;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EmployeeManagement.Tests.Handlers
{
    public class GetEmployeesQueryHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly GetEmployeesQueryHandler _handler;

        public GetEmployeesQueryHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
                .Options;

            _context = new ApplicationDbContext(options);
            _handler = new GetEmployeesQueryHandler(_context);

            // Seed test data
            SeedTestData();
        }

        private void SeedTestData()
        {
            var position1 = new Position 
            { 
                Id = 1, 
                Title = "Developer", 
                BaseSalary = 50000m,
                Description = "Software Developer Position"
            };
            var position2 = new Position 
            { 
                Id = 2, 
                Title = "Manager", 
                BaseSalary = 70000m,
                Description = "Management Position"
            };

            _context.Positions.AddRange(position1, position2);

            var employees = new[]
            {
                new Employee 
                { 
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    MiddleName = "Robert",
                    DateOfBirth = DateTime.UtcNow.AddYears(-30),
                    HireDate = DateTime.UtcNow.AddYears(-5),
                    Position = position1
                },
                new Employee 
                { 
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    MiddleName = "Marie",
                    DateOfBirth = DateTime.UtcNow.AddYears(-35),
                    HireDate = DateTime.UtcNow.AddYears(-7),
                    Position = position2
                }
            };

            _context.Employees.AddRange(employees);
            _context.SaveChanges();
        }

        [Fact]
        public async Task Handle_ReturnsCorrectPageSize()
        {
            // Arrange
            var query = new GetEmployeesQuery { PageSize = 1, PageNumber = 1 };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Single(result.Employees);
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(2, result.TotalPages);
        }

        [Fact]
        public async Task Handle_FiltersBySearchTerm()
        {
            // Arrange
            var query = new GetEmployeesQuery { SearchTerm = "John" };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Single(result.Employees);
            var employee = result.Employees.First();
            Assert.Contains("John", employee.FirstName);
            Assert.Equal("Doe", employee.LastName);
        }

        [Fact]
        public async Task Handle_SortsByPosition()
        {
            // Arrange
            var query = new GetEmployeesQuery { SortBy = "position", SortDescending = false };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            var employees = result.Employees.ToList();
            Assert.Equal("Developer", employees.First().Position.Title);
            Assert.Equal("Manager", employees.Last().Position.Title);
        }
    }
} 