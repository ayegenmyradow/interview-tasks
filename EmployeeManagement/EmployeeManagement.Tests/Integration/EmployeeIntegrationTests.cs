using EmployeeManagement.Core.CQRS.Commands;
using EmployeeManagement.Core.CQRS.Queries;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.Handlers.Commands;
using EmployeeManagement.Infrastructure.Handlers.Queries;
using EmployeeManagement.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EmployeeManagement.Tests.Integration
{
    public class EmployeeIntegrationTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly CreateEmployeeCommandHandler _createHandler;
        private readonly UpdateEmployeeCommandHandler _updateHandler;
        private readonly GetEmployeesQueryHandler _getEmployeesHandler;
        private readonly GetEmployeeByIdQueryHandler _getByIdHandler;

        public EmployeeIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "IntegrationTestDb_" + Guid.NewGuid())
                .Options;

            _context = new ApplicationDbContext(options);
            _createHandler = new CreateEmployeeCommandHandler(_context);
            _updateHandler = new UpdateEmployeeCommandHandler(_context);
            _getEmployeesHandler = new GetEmployeesQueryHandler(_context);
            _getByIdHandler = new GetEmployeeByIdQueryHandler(_context);

            // Seed initial test data
            var position = TestDataFactory.CreateTestPosition();
            _context.Positions.Add(position);
            _context.SaveChanges();
        }

        [Fact]
        public async Task FullEmployeeLifecycle_Success()
        {
            // Create Employee
            var createCommand = TestDataFactory.CreateValidCreateEmployeeCommand();
            var createdEmployee = await _createHandler.Handle(createCommand, CancellationToken.None);
            Assert.NotNull(createdEmployee);
            Assert.Equal(createCommand.FirstName, createdEmployee.FirstName);

            // Get Employee by ID
            var getByIdQuery = new GetEmployeeByIdQuery(createdEmployee.Id);
            var retrievedEmployee = await _getByIdHandler.Handle(getByIdQuery, CancellationToken.None);
            Assert.NotNull(retrievedEmployee);
            Assert.Equal(createdEmployee.Id, retrievedEmployee.Id);

            // Update Employee
            var updateCommand = TestDataFactory.CreateValidUpdateEmployeeCommand(createdEmployee.Id);
            var updatedEmployee = await _updateHandler.Handle(updateCommand, CancellationToken.None);
            Assert.NotNull(updatedEmployee);
            Assert.Equal(updateCommand.FirstName, updatedEmployee.FirstName);

            // Get Updated Employee
            retrievedEmployee = await _getByIdHandler.Handle(getByIdQuery, CancellationToken.None);
            Assert.Equal(updateCommand.FirstName, retrievedEmployee.FirstName);

            // List Employees
            var listQuery = new GetEmployeesQuery { PageSize = 10, PageNumber = 1 };
            var employeesList = await _getEmployeesHandler.Handle(listQuery, CancellationToken.None);
            Assert.Single(employeesList.Employees);
            Assert.Equal(updatedEmployee.Id, employeesList.Employees.First().Id);
        }

        [Fact]
        public async Task SearchAndFilter_ReturnsCorrectResults()
        {
            // Arrange - Create multiple employees
            await _createHandler.Handle(TestDataFactory.CreateValidCreateEmployeeCommand(), CancellationToken.None);
            var secondPosition = TestDataFactory.CreateTestPosition(2);
            _context.Positions.Add(secondPosition);
            await _context.SaveChangesAsync();

            var secondEmployee = TestDataFactory.CreateValidCreateEmployeeCommand(secondPosition.Id);
            secondEmployee.FirstName = "Unique";
            await _createHandler.Handle(secondEmployee, CancellationToken.None);

            // Act - Search by name
            var searchQuery = new GetEmployeesQuery { SearchTerm = "Unique" };
            var searchResults = await _getEmployeesHandler.Handle(searchQuery, CancellationToken.None);

            // Assert
            Assert.Single(searchResults.Employees);
            Assert.Equal("Unique", searchResults.Employees.First().FirstName);
        }

        [Fact]
        public async Task Pagination_ReturnsCorrectPages()
        {
            // Arrange - Create multiple employees
            for (int i = 0; i < 5; i++)
            {
                var command = TestDataFactory.CreateValidCreateEmployeeCommand();
                command.FirstName = $"Employee{i}";
                await _createHandler.Handle(command, CancellationToken.None);
            }

            // Act - Get first page
            var firstPageQuery = new GetEmployeesQuery { PageSize = 2, PageNumber = 1 };
            var firstPage = await _getEmployeesHandler.Handle(firstPageQuery, CancellationToken.None);

            // Assert
            Assert.Equal(2, firstPage.Employees.Count());
            Assert.Equal(5, firstPage.TotalCount);
            Assert.Equal(3, firstPage.TotalPages);
            Assert.True(firstPage.HasNextPage);
            Assert.False(firstPage.HasPreviousPage);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
} 