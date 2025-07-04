using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Core.CQRS.Commands;
using EmployeeManagement.Core.CQRS.DTOs;
using EmployeeManagement.Core.Exceptions;
using EmployeeManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Handlers.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeDto>
    {
        private readonly ApplicationDbContext _context;

        public UpdateEmployeeCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (employee == null)
            {
                throw new NotFoundException($"Employee with ID {request.Id} not found.");
            }

            var position = await _context.Positions.FindAsync(request.PositionId);
            if (position == null)
            {
                throw new NotFoundException($"Position with ID {request.PositionId} not found.");
            }

            // Update employee properties
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.Email = request.Email;
            employee.DateOfBirth = request.DateOfBirth;
            employee.Salary = request.Salary;
            employee.PositionId = request.PositionId;

            await _context.SaveChangesAsync(cancellationToken);

            // Reload the position to get updated data
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