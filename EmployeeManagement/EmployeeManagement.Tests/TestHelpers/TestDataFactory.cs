using EmployeeManagement.Core.CQRS.Commands;
using EmployeeManagement.Core.Models;
using System;

namespace EmployeeManagement.Tests.TestHelpers
{
    public static class TestDataFactory
    {
        public static Position CreateTestPosition(int id = 1)
        {
            return new Position
            {
                Id = id,
                Name = $"Test Position {id}",
                Department = $"Department {id}",
                BaseSalary = 50000m + (id * 1000),
                Description = $"Test Position Description {id}"
            };
        }

        public static Employee CreateTestEmployee(int id = 1, Position? position = null)
        {
            position ??= CreateTestPosition(id);

            return new Employee
            {
                Id = id,
                FirstName = $"FirstName{id}",
                LastName = $"LastName{id}",
                Email = $"employee{id}@test.com",
                DateOfBirth = DateTime.UtcNow.AddYears(-30),
                Salary = position.BaseSalary,
                PositionId = position.Id,
                Position = position
            };
        }

        public static CreateEmployeeCommand CreateValidCreateEmployeeCommand(int positionId = 1)
        {
            return new CreateEmployeeCommand
            {
                FirstName = "Test",
                LastName = "Employee",
                Email = "test.employee@test.com",
                DateOfBirth = DateTime.UtcNow.AddYears(-25),
                Salary = 60000m,
                PositionId = positionId
            };
        }

        public static UpdateEmployeeCommand CreateValidUpdateEmployeeCommand(int id = 1, int positionId = 1)
        {
            return new UpdateEmployeeCommand
            {
                Id = id,
                FirstName = "Updated",
                LastName = "Employee",
                Email = "updated.employee@test.com",
                DateOfBirth = DateTime.UtcNow.AddYears(-25),
                Salary = 65000m,
                PositionId = positionId
            };
        }
    }
} 