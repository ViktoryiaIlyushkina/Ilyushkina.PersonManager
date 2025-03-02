using Ilyushkina.PersonManager.Data.Context;
using Ilyushkina.PersonManager.Data.Models;
using Ilyushkina.PersonManager.Logic.Interfaces;
using Ilyushkina.PersonManager.Logic.Managers;
using Microsoft.EntityFrameworkCore;

namespace Ilyushkina.PersonManager.ConsoleUI
{
   public class Program
    {
        static async Task Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
             .UseSqlite("Data Source=helloapp.db")
             .Options;
            ApplicationContext context = new ApplicationContext(options);
            IEmployeeManager employeeManager = new EmployeeManager(context);

            Person tom = new Person { Name = "Tom", Age = 33 };
            Person alice = new Person { Name = "Alice", Age = 26 };
            var people = new List<Person>();

            people = await employeeManager.Add(tom);
            people = await employeeManager.Add(alice);

            foreach (Person p in people)
            {
                Console.WriteLine($"{p.Id}.{p.Name} - {p.Age}");
            }




        }
    }
}
