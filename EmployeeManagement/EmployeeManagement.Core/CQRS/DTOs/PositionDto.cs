namespace EmployeeManagement.Core.CQRS.DTOs;

/// <summary>
/// Data Transfer Object for Position information
/// </summary>
public class PositionDto
{
    /// <summary>
    /// Unique identifier for the position
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the position
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Department this position belongs to
    /// </summary>
    public string Department { get; set; } = string.Empty;

    /// <summary>
    /// Base salary for this position
    /// </summary>
    public decimal BaseSalary { get; set; }

    /// <summary>
    /// Description of the position
    /// </summary>
    public string Description { get; set; } = string.Empty;
} 