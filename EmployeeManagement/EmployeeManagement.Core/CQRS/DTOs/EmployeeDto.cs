using System;

namespace EmployeeManagement.Core.CQRS.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public int PositionId { get; set; }
        public required string PositionName { get; set; }
        public required string Department { get; set; }

        // Helper property
        public string FullName => $"{LastName} {FirstName}".Trim();
    }
}