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
    public class GetPositionsQueryHandler : IRequestHandler<GetPositionsQuery, GetPositionsResponse>
    {
        private readonly ApplicationDbContext _context;

        public GetPositionsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetPositionsResponse> Handle(GetPositionsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Positions.AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(p => 
                    p.Name.ToLower().Contains(searchTerm) ||
                    (p.Description != null && p.Description.ToLower().Contains(searchTerm)) ||
                    (p.Department != null && p.Department.ToLower().Contains(searchTerm)));
            }

            // Apply sorting
            query = request.SortBy?.ToLower() switch
            {
                "name" => request.SortDescending ? 
                    query.OrderByDescending(p => p.Name) : 
                    query.OrderBy(p => p.Name),
                "department" => request.SortDescending ? 
                    query.OrderByDescending(p => p.Department) : 
                    query.OrderBy(p => p.Department),
                "salary" => request.SortDescending ? 
                    query.OrderByDescending(p => p.BaseSalary) : 
                    query.OrderBy(p => p.BaseSalary),
                _ => query.OrderBy(p => p.Name)
            };

            // Get total count for pagination
            var totalCount = await query.CountAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            // Apply pagination
            var positions = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new PositionDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Department = p.Department ?? "N/A",
                    BaseSalary = p.BaseSalary,
                    Description = p.Description ?? string.Empty
                })
                .ToListAsync(cancellationToken);

            return new GetPositionsResponse
            {
                Positions = positions,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasNextPage = request.PageNumber < totalPages,
                HasPreviousPage = request.PageNumber > 1
            };
        }
    }
} 