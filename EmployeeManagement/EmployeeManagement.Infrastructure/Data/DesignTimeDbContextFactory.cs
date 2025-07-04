using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EmployeeManagement.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Get connection string from environment variable
            var connectionString = Environment.GetEnvironmentVariable("EMPLOYEE_MANAGEMENT_DB_CONNECTION")
                ?? "Host=localhost;Database=EmployeeManagement;Username=postgres;Password=your_password";

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
} 