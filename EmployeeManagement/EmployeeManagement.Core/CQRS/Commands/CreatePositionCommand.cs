using MediatR;
using System.ComponentModel.DataAnnotations;
using EmployeeManagement.Core.CQRS.DTOs;

namespace EmployeeManagement.Core.CQRS.Commands;

public class CreatePositionCommand : IRequest<PositionDto>
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public required string Name { get; set; }

    [StringLength(100)]
    public string? Department { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Base salary must be a positive number")]
    public decimal BaseSalary { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }
} 