using EmployeeManagement.Core.CQRS.DTOs;
using MediatR;

namespace EmployeeManagement.Core.CQRS.Queries;

public class GetPositionByIdQuery : IRequest<PositionDto?>
{
    public int Id { get; }

    public GetPositionByIdQuery(int id)
    {
        Id = id;
    }
} 