using Ilyushkina.PersonManager.Data.Context;
using Ilyushkina.PersonManager.Data.Models;
using Ilyushkina.PersonManager.Logic.Interfaces;
using Ilyushkina.PersonManager.Logic.Managers;
using Ilyushkina.PersonManager.Services.Interfaces;
using Ilyushkina.PersonManager.Services.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyushkina.PersonManager.ServicesTest
{
    public class CompanyEmployeesServiceUnitTests
    {
        //private readonly ApplicationContext _context;
        private readonly Mock<IEmployeeManager> _mockEmployeeManager;
        private readonly Mock<ICompanyManager> _mockCompanyManager;
        private readonly ICompanyEmployeesService _companyEmployeesService;

        public CompanyEmployeesServiceUnitTests()
        {
            //var guid = Guid.NewGuid();
            //var options = new DbContextOptionsBuilder<ApplicationContext>()
            // .UseInMemoryDatabase(databaseName: $"{guid}_EmployeeManagerInMemoryDatabase")
            // .Options;
            //_context = new ApplicationContext(options);
            _mockEmployeeManager = new Mock<IEmployeeManager>();
            _mockCompanyManager = new Mock<ICompanyManager>();
            _companyEmployeesService = new CompanyEmployeesService(_mockEmployeeManager.Object, _mockCompanyManager.Object);
        }

        [Fact]
        public void CountEmployees_ReturnsZeroCount_WhenNoDataExist()
        {
            //Arrange
            Company company = null;
            _mockEmployeeManager.Setup(x => x.GetAll().Result).Returns(new List<Person>() {  });
            _mockCompanyManager.Setup(x => x.Get(It.IsAny<int>()).Result).Returns(company);
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
            Company microsoft = new Company { Id = 1, Name = "Microsoft" };
            Person tom = new Person { Id = 1, Name = "Tom", Age = 33, Company = microsoft };

            _mockEmployeeManager.Setup(x => x.GetAll().Result).Returns(new List<Person>() { tom });
            _mockCompanyManager.Setup(x => x.Get(It.IsAny<int>()).Result).Returns(microsoft);
            //_context.Companies.Add(microsoft);
            //_context.People.Add(tom);
            //_context.SaveChanges();
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

            _mockEmployeeManager.Setup(x => x.GetAll().Result).Returns(new List<Person>() { tom, alice });
            _mockCompanyManager.Setup(x => x.Get(It.IsAny<int>()).Result).Returns(microsoft);
            //_context.Companies.Add(microsoft);
            //_context.Companies.Add(google);
            //_context.People.Add(tom);
            //_context.People.Add(alice);
            //_context.SaveChanges();
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
            Company company = null;
            Company microsoft = new Company { Name = "Microsoft" };
            Company google = new Company { Name = "Google" };
            Person tom = new Person { Name = "Tom", Age = 33, Company = microsoft };
            Person alice = new Person { Name = "Alice", Age = 26, Company = google };

            _mockEmployeeManager.Setup(x => x.GetAll().Result).Returns(new List<Person>() { tom, alice });
            _mockCompanyManager.Setup(x => x.Get(It.IsAny<int>()).Result).Returns(company);
            //_context.Companies.Add(microsoft);
            //_context.Companies.Add(google);
            //_context.People.Add(tom);
            //_context.People.Add(alice);
            //_context.SaveChanges();
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

            _mockEmployeeManager.Setup(x => x.GetAll().Result).Returns(new List<Person>() { });
            _mockCompanyManager.Setup(x => x.Get(It.IsAny<int>()).Result).Returns(microsoft);
            //_context.Companies.Add(microsoft);
            //_context.SaveChanges();
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
            Company company = null;
            Person tom = new Person { Name = "Tom", Age = 33, Company = null };

            _mockEmployeeManager.Setup(x => x.GetAll().Result).Returns(new List<Person>() { tom });
            _mockCompanyManager.Setup(x => x.Get(It.IsAny<int>()).Result).Returns(company);
            //_context.People.Add(tom);
            //_context.SaveChanges();
            var testCompanyId = 1;

            //Act
            var res = _companyEmployeesService.CountEmployeesAsync(testCompanyId).GetAwaiter().GetResult();

            //Assert
            Assert.Equal(0, res);
        }

    }
}
