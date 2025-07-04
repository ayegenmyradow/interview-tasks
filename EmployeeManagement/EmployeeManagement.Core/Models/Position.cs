using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Core.Models
{
    /// <summary>
    /// Represents a position in the organization
    /// </summary>
    public class Position
    {
        public Position()
        {
            Employees = new List<Employee>();
        }

        /// <summary>
        /// Unique identifier for the position
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the position
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Department this position belongs to
        /// </summary>
        [MaxLength(100)]
        public string? Department { get; set; }

        /// <summary>
        /// Base salary for this position
        /// </summary>
        [Required]
        public decimal BaseSalary { get; set; }

        /// <summary>
        /// Description of the position
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }

        // Navigation property
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
} 