using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Core.CQRS.DTOs;
using EmployeeManagement.Core.CQRS.Queries;
using EmployeeManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Handlers.Queries
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, GetEmployeesResponse>
    {
        private readonly ApplicationDbContext _context;

        public GetEmployeesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetEmployeesResponse> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Employees
                .Include(e => e.Position)
                .Where(e => (DateTime.Now - e.DateOfBirth).TotalDays / 365.25 < 70 && e.Salary >= 10000)
                .AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(e =>
                    e.FirstName.ToLower().Contains(searchTerm) ||
                    e.LastName.ToLower().Contains(searchTerm) ||
                    e.Email.ToLower().Contains(searchTerm) ||
                    e.Position.Name.ToLower().Contains(searchTerm) ||
                    (e.Position.Department != null && e.Position.Department.ToLower().Contains(searchTerm))
                );
            }

            // Apply sorting
            query = request.SortBy?.ToLower() switch
            {
                "name" => request.SortDescending 
                    ? query.OrderByDescending(e => e.LastName).ThenByDescending(e => e.FirstName)
                    : query.OrderBy(e => e.LastName).ThenBy(e => e.FirstName),
                "email" => request.SortDescending
                    ? query.OrderByDescending(e => e.Email)
                    : query.OrderBy(e => e.Email),
                "position" => request.SortDescending
                    ? query.OrderByDescending(e => e.Position.Name)
                    : query.OrderBy(e => e.Position.Name),
                "department" => request.SortDescending
                    ? query.OrderByDescending(e => e.Position.Department)
                    : query.OrderBy(e => e.Position.Department),
                "salary" => request.SortDescending
                    ? query.OrderByDescending(e => e.Salary)
                    : query.OrderBy(e => e.Salary),
                _ => query.OrderBy(e => e.Id)
            };

            var totalCount = await query.CountAsync(cancellationToken);
            var totalPages = (totalCount + request.PageSize - 1) / request.PageSize;

            var employees = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    DateOfBirth = e.DateOfBirth,
                    HireDate = e.HireDate,
                    Salary = e.Salary < 15000 ? 15000 : e.Salary,
                    PositionId = e.PositionId,
                    PositionName = e.Position.Name,
                    Department = e.Position.Department ?? "N/A"
                })
                .ToListAsync(cancellationToken);

            return new GetEmployeesResponse
            {
                Employees = employees,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasNextPage = request.PageNumber < totalPages,
                HasPreviousPage = request.PageNumber > 1
            };
        }
    }
} 