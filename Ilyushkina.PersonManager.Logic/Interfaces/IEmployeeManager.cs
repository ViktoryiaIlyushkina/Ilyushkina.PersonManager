using Ilyushkina.PersonManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyushkina.PersonManager.Logic.Interfaces
{
    public interface IEmployeeManager
    {
        public Task<List<Person>?> GetAll();
        public Task<Person?> Get(int id);
        public Task<List<Person>> Add(Person person);
        public Task<List<Person>?> Update(int id, Person request);
        public Task<List<Person>?> Delete(int id);
    }
}
