using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Core.CQRS.Commands;
using EmployeeManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Core.Exceptions;

namespace EmployeeManagement.Infrastructure.Handlers.Commands
{
    public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public DeletePositionCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
        {
            var position = await _context.Positions
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (position == null)
                throw new NotFoundException($"Position with ID {request.Id} not found");

            if (position.Employees.Any())
                throw new BusinessRuleException("Cannot delete position that has employees assigned");

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
} 