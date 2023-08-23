using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Organization.Contracts;
using Organization.Models;
using Organization.Services;

namespace Organization.Tests
{
    [TestClass]
    public class EmployeeServiceUnitTest
    {
        private EmployeeDbContext _context;
        private IEmployeeService _employeeService;
        private IFixture _fixture;

        [TestInitialize]
        public void TestInit()
        {
            _fixture = new Fixture();
            _context = new EmployeeDbContext(new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
            _employeeService = new EmployeeService(_context);
        }

        [TestMethod]
        public async Task GetEmployeesTest()
        {
            //Arrange
            var employee = _fixture.Build<Employee>()
                .Create();
            await _context.Set<Employee>().AddAsync(employee);
            await _context.SaveChangesAsync();

            //Act
            var employeeList = await _employeeService.GetEmployees();

            //Assert
            Assert.IsNotNull(employeeList);
        }

        [TestMethod]
        public async Task GetEmployeesByIdWhenIdExists()
        {
            //Arrange
            var employee = _fixture.Build<Employee>()
                .With(x => x.EmployeeId, 3)
                .Create();
            await _context.Set<Employee>().AddAsync(employee);
            await _context.SaveChangesAsync();

            //Act
            var employeeSearch = await _employeeService.GetEmployeeById(employee.EmployeeId);

            //Assert
            Assert.IsNotNull(employeeSearch);
            Assert.AreEqual(employeeSearch.Email, employee.Email);
        }

        [TestMethod]
        public async Task GetEmployeesByIdWhenIdDoesNotExists()
        {
            //Arrange
            var employee = _fixture.Build<Employee>()
                .With(x => x.EmployeeId, 3)
                .Create();
            await _context.Set<Employee>().AddAsync(employee);
            await _context.SaveChangesAsync();

            //Act
            var employeeSearch = await _employeeService.GetEmployeeById(1);

            //Assert
            Assert.IsNull(employeeSearch);
        }

        [TestMethod]
        public async Task GetEmployeesWithSameFirstOrLastNameWhenNameExists()
        {
            //Arrange
            var employee = _fixture.Build<Employee>()
                .With(x => x.FirstName, "John")
                .Create();
            await _context.Set<Employee>().AddAsync(employee);
            await _context.SaveChangesAsync();

            //Act
            var employeeList = await _employeeService.GetEmployeesWithSameFirstOrLastName(employee.FirstName);

            //Assert
            Assert.IsNotNull(employeeList);
        }

        [TestMethod]
        public async Task GetEmployeesWithSameFirstOrLastNameWhenNameDoesNotExists()
        {
            //Arrange
            var employee = _fixture.Build<Employee>()
                .With(x => x.FirstName, "John")
                .Create();
            await _context.Set<Employee>().AddAsync(employee);
            await _context.SaveChangesAsync();

            //Act
            var employeeList = await _employeeService.GetEmployeesWithSameFirstOrLastName("Megan");

            //Assert
            Assert.IsNull(employeeList);
        }

        [TestMethod]
        public async Task AddEmployeeWithDuplicateRecord()
        {
            //Arrange
            var employee = _fixture.Build<Employee>()
                .With(x => x.FirstName, "John")
                .With(x => x.LastName, "Smith")
                .With(x => x.Email, "john@gmail.com")
                .Create();
            await _context.Set<Employee>().AddAsync(employee);
            await _context.SaveChangesAsync();

            //Act
            var employeeInsert = await _employeeService.AddEmployee(employee);

            //Assert
            Assert.IsNull(employeeInsert);
        }

        [TestMethod]
        public async Task AddEmployeeWithoutDuplicateRecord()
        {
            //Arrange
            var employee = _fixture.Build<Employee>()
                .With(x => x.FirstName, "John")
                .With(x => x.LastName, "Smith")
                .With(x => x.Email, "john@gmail.com")
                .Create();
            await _context.Set<Employee>().AddAsync(employee);
            await _context.SaveChangesAsync();

            var newEmployee = new Employee
            {
                FirstName = "Megan",
                LastName = "Fox",
                Email = "megan@gmail.com",
                Age = 30,
                Address = "Pittsburgh"
            };

            //Act
            var employeeInsert = await _employeeService.AddEmployee(newEmployee);

            //Assert
            Assert.IsNotNull(employeeInsert);
            Assert.AreEqual(employeeInsert.FirstName, newEmployee.FirstName);
        }

        [TestMethod]
        public async Task UpdateEmployeeWhenIdExists()
        {
            //Arrange
            var employee = _fixture.Build<Employee>()
                .With(x => x.EmployeeId, 3)
                .Create();
            await _context.Set<Employee>().AddAsync(employee);
            await _context.SaveChangesAsync();

            employee.Age = 40;
            //Act
            var employeeSearch = await _employeeService.UpdateEmployee(employee.EmployeeId, employee);

            //Assert
            Assert.IsNotNull(employeeSearch);
            Assert.AreEqual(employeeSearch.Age, employee.Age);
        }

        [TestMethod]
        public async Task UpdateEmployeeWhenIdDoesNotExists()
        {
            //Arrange
            var employee = _fixture.Build<Employee>()
                .With(x => x.EmployeeId, 3)
                .Create();
            await _context.Set<Employee>().AddAsync(employee);
            await _context.SaveChangesAsync();

            employee.Age = 40;
            //Act
            var employeeSearch = await _employeeService.UpdateEmployee(2, employee);

            //Assert
            Assert.IsNull(employeeSearch);
        }

        [TestMethod]
        public async Task DeleteEmployeeWhenIdExists()
        {
            //Arrange
            var employee = _fixture.Build<Employee>()
                .With(x => x.EmployeeId, 3)
                .Create();
            await _context.Set<Employee>().AddAsync(employee);
            await _context.SaveChangesAsync();

            //Act
            var employeeSearch = await _employeeService.DeleteEmployee(employee.EmployeeId);

            //Assert
            Assert.AreEqual(true, employeeSearch);
        }

        [TestMethod]
        public async Task DeleteEmployeeWhenIdDoesNotExists()
        {
            //Arrange
            var employee = _fixture.Build<Employee>()
                .With(x => x.EmployeeId, 3)
                .Create();
            await _context.Set<Employee>().AddAsync(employee);
            await _context.SaveChangesAsync();

            //Act
            var employeeSearch = await _employeeService.DeleteEmployee(2);

            //Assert
            Assert.AreEqual(false, employeeSearch);
        }
    }
}