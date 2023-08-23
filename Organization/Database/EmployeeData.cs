using Microsoft.EntityFrameworkCore;
using Organization.Models;

namespace Organization.Database
{
    public static class EmployeeData
    {
        public static void AddEmployeeRows(IServiceProvider serviceProvider)
        {
            using var context = new EmployeeDbContext(serviceProvider.GetRequiredService<DbContextOptions<EmployeeDbContext>>());
            if (context.Employees.Any())
            {
                return;
            }
            context.Employees.AddRange(
                new Employee { FirstName = "John", LastName = "Wick", Email = "john@gmail.com", Age = 34, Address = "24/5, West st.,Pittsburgh" },
                new Employee { FirstName = "Megan", LastName = "Fox", Email = "megan@gmail.com", Age = 30, Address = "10/2, Joyana rd, New York" }
                );
            context.SaveChanges();
        }
    }
}