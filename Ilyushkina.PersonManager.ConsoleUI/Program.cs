using Ilyushkina.PersonManager.Data.Context;
using Ilyushkina.PersonManager.Data.Models;
using Ilyushkina.PersonManager.Logic.Interfaces;
using Ilyushkina.PersonManager.Logic.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Entity;

namespace Ilyushkina.PersonManager.ConsoleUI
{
   public class Program
    {
        static async Task Main(string[] args)
        {
            //var serviceProvider = new ServiceCollection()
            //    .AddDbContext<ApplicationContext>(options =>
            //    options.UseSqlite("Data Source=helloapp.db"))
            //    .AddScoped<IEmployeeManager, EmployeeManager>()
            //    .BuildServiceProvider();

            //var context = serviceProvider.GetRequiredService<ApplicationContext>();
            //var employeeManager = serviceProvider.GetRequiredService<IEmployeeManager>();

            var options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlite("Data Source=helloapp.db");
            ApplicationContext context = new ApplicationContext(options.Options);
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
