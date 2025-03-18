using Ilyushkina.PersonManager.Data.Context;
using Ilyushkina.PersonManager.Data.Models;
using Ilyushkina.PersonManager.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ilyushkina.PersonManager.Logic.Managers
{
    public class EmployeeManager: IEmployeeManager
    {
        private readonly ApplicationContext _context;

        public EmployeeManager(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Person>?> GetAll()
        {
            var people = await _context.People.ToListAsync();
            return people;
        }

        public async Task<Person?> Get(int id)
        {
            var person = await _context.People.FindAsync(id);

            if (person is null)
            {
                return null;
            }

            return person;
        }

        public async Task<List<Person>> Add(Person person)
        {
            _context.People.Add(person);

            await _context.SaveChangesAsync();

            return await _context.People.Include(p => p.Company).AsNoTracking().ToListAsync();
        }

        public async Task<List<Person>?> Update(int id, Person request)
        {
            var person = await _context.People.FindAsync(id);

            if (person is null)
            {
                return null;
            }

            person.Name = request.Name;
            person.Age = request.Age;

            await _context.SaveChangesAsync();

            return await _context.People.ToListAsync();
        }
        public async Task<List<Person>?> Delete(int id)
        {
            var person = await _context.People.FindAsync(id);

            if (person is null)
            {
                return null;
            }

            _context.People.Remove(person);

            await _context.SaveChangesAsync();

            return await _context.People.ToListAsync();
        }
    }
}
