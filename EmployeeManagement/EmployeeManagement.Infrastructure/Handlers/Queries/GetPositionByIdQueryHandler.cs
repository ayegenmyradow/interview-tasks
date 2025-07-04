using EmployeeManagement.Core.CQRS.DTOs;
using EmployeeManagement.Core.CQRS.Queries;
using EmployeeManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Handlers.Queries;

public class GetPositionByIdQueryHandler : IRequestHandler<GetPositionByIdQuery, PositionDto?>
{
    private readonly ApplicationDbContext _context;

    public GetPositionByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PositionDto?> Handle(GetPositionByIdQuery request, CancellationToken cancellationToken)
    {
        var position = await _context.Positions
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (position == null)
            return null;

        return new PositionDto
        {
            Id = position.Id,
            Name = position.Name,
            Department = position.Department ?? "N/A",
            BaseSalary = position.BaseSalary,
            Description = position.Description ?? string.Empty
        };
    }
} 