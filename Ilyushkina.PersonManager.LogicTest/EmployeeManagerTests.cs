using Ilyushkina.PersonManager.Data.Context;
using Ilyushkina.PersonManager.Data.Models;
using Ilyushkina.PersonManager.Logic.Interfaces;
using Ilyushkina.PersonManager.Logic.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ilyushkina.PersonManager.LogicTest
{
    public class EmployeeManagerTests
    {
        private readonly ApplicationContext _context;
        private readonly IEmployeeManager _employeeManager;

        // с использованием DI
        //public EmployeeManagerTests() 
        //{
        //    var serviceProvider = new ServiceCollection()
        //    .AddDbContext<ApplicationContext>(options =>
        //    options.UseInMemoryDatabase(
        //    $"{nameof(EmployeeManagerTests)}_DataBase",
        //    builder => builder.EnableNullChecks(false))
        //    .UseInternalServiceProvider(
        //        new ServiceCollection()
        //            .AddEntityFrameworkInMemoryDatabase()
        //            .BuildServiceProvider()))
        //    //.AddScoped<IEmployeeManager, EmployeeManager>()
        //    .BuildServiceProvider();

        //    _context  = serviceProvider.GetRequiredService<ApplicationContext>();
        //    _employeeManager = new EmployeeManager(_context);
        //}

        public EmployeeManagerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
             .UseInMemoryDatabase(databaseName: "EmployeeManagerInMemoryDatabase")
             .Options;
            _context = new ApplicationContext(options);
            _employeeManager = new EmployeeManager(_context);
        }

        [Fact]
        public void GetAll_NotEmptyDatabase_Returns_ListOfPeople()
        {
            // Arrange
            //var options = new DbContextOptionsBuilder<ApplicationContext>()
            //  .UseInMemoryDatabase(databaseName: "SalonDatabase")
            //  .Options;
            //ApplicationContext context = new ApplicationContext(options);
            Person tom = new Person { Name = "Tom", Age = 33 };
            Person alice = new Person { Name = "Alice", Age = 26 };
            _context.People.Add(tom);
            _context.People.Add(alice);
            _context.SaveChanges();
            var expectedList = new List<Person>();
            expectedList.Add(tom);
            expectedList.Add(alice);

            // Act
            //IEmployeeManager employeeManager = new EmployeeManager(context);
            var result = _employeeManager.GetAll().GetAwaiter().GetResult();
            
            //Assert
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.Equal(expectedList, result);

        }

        [Fact]
        public void Get_PersonId_Returns_Person()
        {
            // Arrange
            Person tom = new Person { Id = 1, Name = "Tom", Age = 33 };
            Person alice = new Person { Id = 2, Name = "Alice", Age = 26 };
            var testId = 1;
            _context.People.Add(tom);
            _context.People.Add(alice);
            _context.SaveChanges();

            // Act
            var result = _employeeManager.Get(testId).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(testId, result.Id);
        }

        [Fact]
        public void Add_Person_Returns_ListOfPeopleWithPerson()
        {
            // Arrange
            Person tom = new Person { Id = 1, Name = "Tom", Age = 33 };

            // Act
            var result = _employeeManager.Add(tom).GetAwaiter().GetResult();
            _context.SaveChanges();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(result[0].Name, tom.Name);
        }


        [Fact]
        public void Update_PersonIdAndPersonWithRequestUpdates_Returns_ListOfPeopleWithUpdetedPerson()
        {
            // Arrange
            Person tom = new Person { Id = 1, Name = "Tom", Age = 33 };
            _context.People.Add(tom);
            _context.SaveChanges();
            Person updatedTom = new Person { Id = 1, Name = "Tom", Age = 24 };
            var id = 1;

            // Act
            var result = _employeeManager.Update(id, updatedTom).GetAwaiter().GetResult();
            _context.SaveChanges();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(result[0].Age, updatedTom.Age);
        }

        [Fact]
        public void Delete_PersonId_Returns_ListOfPeopleWithoutPerson()
        {
            // Arrange
            Person tom = new Person { Id = 1, Name = "Tom", Age = 33 };
            Person alice = new Person { Id = 2, Name = "Alice", Age = 26 };
            _context.People.Add(tom);
            _context.People.Add(alice);
            _context.SaveChanges();
            var deleteId = tom.Id;
            var expectedId = alice.Id;

            // Act
            var result = _employeeManager.Delete(deleteId).GetAwaiter().GetResult();
            _context.SaveChanges();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(result[0].Id, expectedId);
        }
    }
}