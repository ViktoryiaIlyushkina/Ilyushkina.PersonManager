using Ilyushkina.PersonManager.Data.Context;
using Ilyushkina.PersonManager.Data.Models;
using Ilyushkina.PersonManager.Logic.Interfaces;
using Ilyushkina.PersonManager.Logic.Managers;
using Ilyushkina.PersonManager.Services.Interfaces;
using Ilyushkina.PersonManager.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Entity;

namespace Ilyushkina.PersonManager.ConsoleUI
{
   public class Program
    {
        static async Task Main(string[] args)
        {
    
            var applicationContextFactory = new ApplicationContextFactory();
            ApplicationContext context = applicationContextFactory.CreateDbContext(null);
            await context.Database.MigrateAsync();
            IEmployeeManager employeeManager = new EmployeeManager(context);
            ICompanyManager companyManager = new CompanyManager(context);
            ICompanyEmployeesService companyEmployeesService = new CompanyEmployeesService(employeeManager, companyManager);

            Company microsoft = new Company { Name = "Microsoft" };
            Company google = new Company { Name = "Google" };
            Person tom = new Person { Name = "Tom", Age = 33, Company = microsoft };
            Person alice = new Person { Name = "Alice", Age = 26, Company = google };
            var people = new List<Person>();
            var companies = new List<Company>();

            companies = await companyManager.Add(microsoft);
            companies = await companyManager.Add(google);
            people = await employeeManager.Add(tom);
            people = await employeeManager.Add(alice);
         

            foreach (Person p in people)
            {
                Console.WriteLine($"{p.Id}.{p.Name} - {p.Age} - {p.Company.Name}");
            }

            foreach (Company c in companies)
            {
                Console.WriteLine($"{c.Id}.{c.Name}");
            }

            Console.WriteLine($"Employees in Microsoft: {companyEmployeesService.CountEmployeesAsync(microsoft.Id)}");
        }
        public class ApplicationContextFactory: IDesignTimeDbContextFactory<ApplicationContext>
        {
            public ApplicationContext CreateDbContext(string[]? args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
                optionsBuilder.UseSqlite("Data Source=personManagerApp.db");

                return new ApplicationContext(optionsBuilder.Options);
            }
        }
    }
}
