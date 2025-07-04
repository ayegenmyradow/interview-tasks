using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Core.Models
{
    /// <summary>
    /// Represents an employee in the organization
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Unique identifier for the employee
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Employee's first name
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Employee's last name
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Employee's email address
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Employee's middle name (optional)
        /// </summary>
        [MaxLength(50)]
        public string? MiddleName { get; set; }

        /// <summary>
        /// Employee's date of birth
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Date when the employee was hired
        /// </summary>
        [Required]
        public DateTime HireDate { get; set; }

        /// <summary>
        /// Current salary of the employee
        /// </summary>
        [Required]
        public decimal Salary { get; set; }

        /// <summary>
        /// Foreign key to the employee's position
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// Navigation property for the employee's position
        /// </summary>
        public virtual Position Position { get; set; } = null!;

        /// <summary>
        /// Gets the full name of the employee
        /// </summary>
        /// <returns>Full name including middle name if available</returns>
        public string GetFullName()
        {
            return string.IsNullOrEmpty(MiddleName)
                ? $"{LastName} {FirstName}"
                : $"{LastName} {FirstName} {MiddleName}";
        }
    }
} 