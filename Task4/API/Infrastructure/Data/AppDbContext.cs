using Microsoft.EntityFrameworkCore;
using Task4.Domain.Employees;

namespace Task4.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees => Set<Employee>();
    }

}
