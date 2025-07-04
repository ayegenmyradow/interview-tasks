using System;
using MediatR;
using EmployeeManagement.Core.CQRS.DTOs;

namespace EmployeeManagement.Core.CQRS.Commands
{
    public class UpdateEmployeeCommand : IRequest<EmployeeDto>
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Salary { get; set; }
        public int PositionId { get; set; }
    }
} 