using Ilyushkina.PersonManager.Data.Context;
using Ilyushkina.PersonManager.Data.Models;
using Ilyushkina.PersonManager.Logic.Interfaces;
using Ilyushkina.PersonManager.Logic.Managers;
using Ilyushkina.PersonManager.Services.Interfaces;
using Ilyushkina.PersonManager.Services.Services;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace Ilyushkina.PersonManager.ServicesTest
{
    public class CompanyEmployeesServiceIntegratedTests
    {
        private readonly ApplicationContext _context;
        private readonly IEmployeeManager _employeeManager;
        private readonly ICompanyManager _companyManager;
        private readonly ICompanyEmployeesService _companyEmployeesService;

        public CompanyEmployeesServiceIntegratedTests()
        {
            var guid = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<ApplicationContext>()
             .UseInMemoryDatabase(databaseName: $"{guid}_EmployeeManagerInMemoryDatabase")
             .Options;
            _context = new ApplicationContext(options);
            _employeeManager = new EmployeeManager(_context);
            _companyManager = new CompanyManager(_context);
            _companyEmployeesService = new CompanyEmployeesService(_employeeManager, _companyManager);
        }

        [Fact]
        public void CountEmployees_ReturnsZeroCount_WhenNoDataExist()
        {
            //Arrange
            var testCompanyId = 1;

            //Act
            var res = _companyEmployeesService.CountEmployeesAsync(testCompanyId).GetAwaiter().GetResult();   

            //Assert
            Assert.Equal(0, res);
        }

        [Fact]
        public void CountEmployees_ReturnsEmployeesNumber_WhenDataExists()
        {
            //Arrange
            Company microsoft = new Company { Name = "Microsoft" };
            Person tom = new Person { Name = "Tom", Age = 33, Company = microsoft };
            _context.Companies.Add(microsoft);
            _context.People.Add(tom);
            _context.SaveChanges();
            var testCompanyId = 1;

            //Act
            var res = _companyEmployeesService.CountEmployeesAsync(testCompanyId).GetAwaiter().GetResult();

            //Assert
            Assert.Equal(1, res);
        }

        [Fact]
        public void CountEmployees_ReturnsEmployeesNumber_WhenMoreDataExist()
        {
            //Arrange
            Company microsoft = new Company { Name = "Microsoft" };
            Company google = new Company { Name = "Google" };
            Person tom = new Person { Name = "Tom", Age = 33, Company = microsoft };
            Person alice = new Person { Name = "Alice", Age = 26, Company = google };
            _context.Companies.Add(microsoft);
            _context.Companies.Add(google);
            _context.People.Add(tom);
            _context.People.Add(alice);
            _context.SaveChanges();
            var testCompanyId = 1;

            //Act
            var res = _companyEmployeesService.CountEmployeesAsync(testCompanyId).GetAwaiter().GetResult();

            //Assert
            Assert.Equal(1, res);
        }

        [Fact]
        public void CountEmployees_ReturnsZeroCount_WhenMoreDataExist()
        {
            //Arrange
            Company microsoft = new Company { Name = "Microsoft" };
            Company google = new Company { Name = "Google" };
            Person tom = new Person { Name = "Tom", Age = 33, Company = microsoft };
            Person alice = new Person { Name = "Alice", Age = 26, Company = google };
            _context.Companies.Add(microsoft);
            _context.Companies.Add(google);
            _context.People.Add(tom);
            _context.People.Add(alice);
            _context.SaveChanges();
            var testCompanyId = 3;

            //Act
            var res = _companyEmployeesService.CountEmployeesAsync(testCompanyId).GetAwaiter().GetResult();

            //Assert
            Assert.Equal(0, res);
        }

        [Fact]
        public void CountEmployees_ReturnsZeroCount_WhenEmployeesNotExist()
        {
            //Arrange
            Company microsoft = new Company { Name = "Microsoft" };
            _context.Companies.Add(microsoft);
            _context.SaveChanges();
            var testCompanyId = 1;

            //Act
            var res = _companyEmployeesService.CountEmployeesAsync(testCompanyId).GetAwaiter().GetResult();

            //Assert
            Assert.Equal(0, res);
        }

        [Fact]
        public void CountEmployees_ReturnsZeroCount_WhenCompaniesNotExist()
        {
            //Arrange
            Person tom = new Person { Name = "Tom", Age = 33, Company = null };
            _context.People.Add(tom);
            _context.SaveChanges();
            var testCompanyId = 1;

            //Act
            var res = _companyEmployeesService.CountEmployeesAsync(testCompanyId).GetAwaiter().GetResult();

            //Assert
            Assert.Equal(0, res);
        }
    }
}