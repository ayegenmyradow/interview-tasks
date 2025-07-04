using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Core.CQRS.Commands;
using EmployeeManagement.Core.Exceptions;
using EmployeeManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Handlers.Commands
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public DeleteEmployeeCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(request.Id);
            
            if (employee == null)
                throw new NotFoundException($"Employee with ID {request.Id} not found");

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}