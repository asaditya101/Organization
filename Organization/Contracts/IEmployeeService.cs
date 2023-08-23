using Organization.Models;

namespace Organization.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>?> GetEmployees();
        Task<Employee?> GetEmployeeById(int employeeId);
        Task<IEnumerable<Employee>?> GetEmployeesWithSameFirstOrLastName(string name);
        Task<Employee?> AddEmployee(Employee employee);
        Task<Employee?> UpdateEmployee(int employeeId, Employee employee);
        Task<bool> DeleteEmployee(int employeeId);
    }
}
