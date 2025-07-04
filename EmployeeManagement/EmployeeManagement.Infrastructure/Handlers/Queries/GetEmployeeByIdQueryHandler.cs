using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Core.CQRS.DTOs;
using EmployeeManagement.Core.CQRS.Queries;
using EmployeeManagement.Core.Exceptions;
using EmployeeManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Handlers.Queries
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly ApplicationDbContext _context;

        public GetEmployeeByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (employee == null)
            {
                throw new NotFoundException($"Employee with ID {request.Id} was not found.");
            }

            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                DateOfBirth = employee.DateOfBirth,
                Salary = employee.Salary < 15000 ? 15000 : employee.Salary,
                PositionId = employee.PositionId,
                PositionName = employee.Position.Name,
                Department = employee.Position.Department ?? "N/A"
            };
        }
    }
} 