using MediatR;

namespace EmployeeManagement.Core.CQRS.Commands
{
    public class DeletePositionCommand : IRequest<Unit>
    {
        public int Id { get; }

        public DeletePositionCommand(int id)
        {
            Id = id;
        }
    }
} 