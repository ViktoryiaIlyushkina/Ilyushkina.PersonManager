using Ilyushkina.PersonManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyushkina.PersonManager.Logic.Interfaces
{
    public interface ICompanyManager
    {
        public Task<List<Company>?> GetAll();
        public Task<Company?> Get(int id);
        public Task<List<Company>> Add(Company person);
        public Task<List<Company>?> Update(int id, Company request);
        public Task<List<Company>?> Delete(int id);
    }
}
