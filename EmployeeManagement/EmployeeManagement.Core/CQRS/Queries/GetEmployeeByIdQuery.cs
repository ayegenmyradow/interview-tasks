using MediatR;
using EmployeeManagement.Core.CQRS.DTOs;

namespace EmployeeManagement.Core.CQRS.Queries
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDto>
    {
        public int Id { get; }

        public GetEmployeeByIdQuery(int id)
        {
            Id = id;
        }
    }
}