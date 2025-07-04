using System;
using System.ComponentModel.DataAnnotations;
using EmployeeManagement.Core.CQRS.DTOs;
using MediatR;

namespace EmployeeManagement.Core.CQRS.Commands
{
    public class CreateEmployeeCommand : IRequest<EmployeeDto>
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public decimal Salary { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid position")]
        public int PositionId { get; set; }
    }
} 