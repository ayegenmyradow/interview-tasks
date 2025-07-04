using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Core.CQRS.Commands;
using EmployeeManagement.Core.CQRS.DTOs;
using EmployeeManagement.Core.Models;
using EmployeeManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Handlers.Commands
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
    {
        private readonly ApplicationDbContext _context;

        public CreateEmployeeCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var position = await _context.Positions.FindAsync(request.PositionId);
            if (position == null)
            {
                throw new InvalidOperationException($"Position with ID {request.PositionId} not found.");
            }

            var employee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                DateOfBirth = DateTime.SpecifyKind(request.DateOfBirth.Date, DateTimeKind.Utc),
                HireDate = DateTime.UtcNow,
                Salary = request.Salary,
                PositionId = request.PositionId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);

            // Reload the employee with position data
            await _context.Entry(employee)
                .Reference(e => e.Position)
                .LoadAsync(cancellationToken);

            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                DateOfBirth = employee.DateOfBirth,
                Salary = employee.Salary,
                PositionId = employee.PositionId,
                PositionName = employee.Position.Name,
                Department = employee.Position.Department ?? "N/A"
            };
        }
    }
} 