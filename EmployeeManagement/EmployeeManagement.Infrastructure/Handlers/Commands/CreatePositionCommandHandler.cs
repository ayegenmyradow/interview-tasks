using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Core.CQRS.Commands;
using EmployeeManagement.Core.CQRS.DTOs;
using EmployeeManagement.Core.Models;
using EmployeeManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeManagement.Infrastructure.Handlers.Commands
{
    public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, PositionDto>
    {
        private readonly ApplicationDbContext _context;

        public CreatePositionCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PositionDto> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
        {
            // Check if position with same name already exists
            var exists = await _context.Positions
                .AnyAsync(p => p.Name == request.Name, cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException($"Position with name '{request.Name}' already exists.");
            }

            var position = new Position
            {
                Name = request.Name,
                Department = request.Department ?? string.Empty,
                BaseSalary = request.BaseSalary,
                Description = request.Description ?? string.Empty
            };

            _context.Positions.Add(position);
            await _context.SaveChangesAsync(cancellationToken);

            return new PositionDto
            {
                Id = position.Id,
                Name = position.Name,
                Department = position.Department,
                BaseSalary = position.BaseSalary,
                Description = position.Description
            };
        }
    }
} 