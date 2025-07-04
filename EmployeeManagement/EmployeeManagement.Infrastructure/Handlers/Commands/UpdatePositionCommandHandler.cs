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
    public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, PositionDto>
    {
        private readonly ApplicationDbContext _context;

        public UpdatePositionCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PositionDto> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
        {
            var position = await _context.Positions
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (position == null)
            {
                throw new NotFoundException($"Position with ID {request.Id} not found.");
            }

            // Check if another position with the same name exists
            var exists = await _context.Positions
                .AsNoTracking()
                .AnyAsync(p => p.Name == request.Name && p.Id != request.Id, cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException($"Position with name '{request.Name}' already exists.");
            }

            // Create a new tracked entity
            var updatedPosition = await _context.Positions.FindAsync(new object[] { request.Id }, cancellationToken);
            if (updatedPosition == null)
            {
                throw new NotFoundException($"Position with ID {request.Id} not found.");
            }

            // Update the properties
            updatedPosition.Name = request.Name;
            updatedPosition.Department = request.Department ?? string.Empty;
            updatedPosition.BaseSalary = request.BaseSalary;
            updatedPosition.Description = request.Description ?? string.Empty;

            // Mark the entity as modified
            _context.Entry(updatedPosition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Positions.AnyAsync(p => p.Id == request.Id, cancellationToken))
                {
                    throw new NotFoundException($"Position with ID {request.Id} not found.");
                }
                throw;
            }

            // Return the updated position
            return new PositionDto
            {
                Id = updatedPosition.Id,
                Name = updatedPosition.Name,
                Department = updatedPosition.Department,
                BaseSalary = updatedPosition.BaseSalary,
                Description = updatedPosition.Description
            };
        }
    }
}