namespace Task4.Domain.Employees
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime HiredDate { get; set; }
        public decimal Salary { get; set; }
    }
}
