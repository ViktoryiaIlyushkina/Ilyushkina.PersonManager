using Ilyushkina.PersonManager.Data.Context;
using Ilyushkina.PersonManager.Data.Models;
using Ilyushkina.PersonManager.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyushkina.PersonManager.Logic.Managers
{
    public class CompanyManager : ICompanyManager
    {
        private readonly ApplicationContext _context;

        public CompanyManager(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Company>?> GetAll()
        {
            var companies = await _context.Companies.ToListAsync();
            return companies;
        }

        public async Task<Company?> Get(int id)
        {
            //id += 5;
            var company = await _context.Companies.FindAsync(id);

            if (company is null)
            {
                return null;
            }
            return company;
        }

        public async Task<List<Company>> Add(Company company)
        {
            _context.Companies.Add(company);

            await _context.SaveChangesAsync();

            return await _context.Companies.AsNoTracking().ToListAsync();
        }

        public async Task<List<Company>?> Update(int id, Company request)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company is null)
            {
                return null;
            }

            company.Name = request.Name;

            await _context.SaveChangesAsync();

            return await _context.Companies.ToListAsync();
        }
        public async Task<List<Company>?> Delete(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company is null)
            {
                return null;
            }

            _context.Companies.Remove(company);

            await _context.SaveChangesAsync();

            return await _context.Companies.ToListAsync();
        }
    }
}
