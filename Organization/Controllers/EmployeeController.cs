using Microsoft.AspNetCore.Mvc;
using Organization.Contracts;
using Organization.Models;

namespace Organization.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await _employeeService.GetEmployees();
            return Ok(result);
        }

        [HttpGet]
        [Route("{employeeId}")]
        public async Task<IActionResult> GetEmployeeById(int employeeId)
        {
            var result = await _employeeService.GetEmployeeById(employeeId);
            if (result == null)
            {
                return NotFound("Employee ID Not Found!!!");
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("name")]
        public async Task<IActionResult> GetEmployeesWithSameFirstOrLastName(string name)
        {
            var result = await _employeeService.GetEmployeesWithSameFirstOrLastName(name);
            if (result == null)
            {
                return NotFound("name not found!!!");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            var result = await _employeeService.AddEmployee(employee);
            if (result == null)
            {
                return BadRequest("Duplicate Entry!!!");
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(int employeeId, Employee employee)
        {
            var result = await _employeeService.UpdateEmployee(employeeId, employee);
            if (result == null)
            {
                return NotFound("Employee Id not found!!!");
            }
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var result = await _employeeService.DeleteEmployee(employeeId);
            if (!result)
            {
                return NotFound("Employee ID not found!!!");
            }
            return Ok("Deleted!!!");
        }
    }
}