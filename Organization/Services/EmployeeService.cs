using Microsoft.EntityFrameworkCore;
using Organization.Contracts;
using Organization.Models;

namespace Organization.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext _context;
        public EmployeeService(EmployeeDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Employee>?> GetEmployees()
        {
            var result = await _context.Employees.ToListAsync();
            return result ?? null;
        }

        public async Task<Employee?> GetEmployeeById(int employeeId)
        {
            var employeeSearch = await _context.Employees.Where(x => x.EmployeeId == employeeId).FirstOrDefaultAsync();
            return employeeSearch ?? null;
        }

        public async Task<IEnumerable<Employee>?> GetEmployeesWithSameFirstOrLastName(string name)
        {
            var employeeList = await _context.Employees.Where(x => x.FirstName == name || x.LastName == name).ToListAsync();
            return employeeList.Count > 0 ? employeeList : null;
        }

        public async Task<Employee?> AddEmployee(Employee employee)
        {
            var empSearch = await _context.Employees.Where(x => x.FirstName == employee.FirstName
            && x.LastName == employee.LastName && x.Email == employee.Email).FirstOrDefaultAsync();
            if (empSearch != null)
            {
                return null;
            }
            employee.EmployeeId = 0;
            await _context.Set<Employee>().AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> UpdateEmployee(int employeeId, Employee employee)
        {
            var employeeSearch = await _context.Employees.Where(x => x.EmployeeId == employeeId).FirstOrDefaultAsync();
            if (employeeSearch == null)
            {
                return null;
            }
            employeeSearch.FirstName = employee.FirstName;
            employeeSearch.LastName = employee.LastName;
            employeeSearch.Email = employee.Email;
            employeeSearch.Age = employee.Age;
            employeeSearch.Address = employee.Address;
            await _context.SaveChangesAsync();
            return employeeSearch;
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            var employeeSearch = await _context.Employees.Where(x => x.EmployeeId == employeeId).FirstOrDefaultAsync();
            if (employeeSearch == null)
            {
                return false;
            }
            _context.Employees.Remove(employeeSearch);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}