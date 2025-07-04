using MediatR;

namespace EmployeeManagement.Core.CQRS.Commands
{
    public class DeleteEmployeeCommand : IRequest<Unit>
    {
        public int Id { get; }

        public DeleteEmployeeCommand(int id)
        {
            Id = id;
        }
    }
} 