
namespace Task4.Domain.Employees.Queries
{
    public class GetEmployeesQuery : IRequest<List<Employee>>
    {
        public string? Filter { get; set; }
        public string? SortBy { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
