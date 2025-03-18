using Ilyushkina.PersonManager.Logic.Interfaces;
using Ilyushkina.PersonManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyushkina.PersonManager.Services.Services
{
    public class CompanyEmployeesService : ICompanyEmployeesService
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly ICompanyManager _companyManager;

        public CompanyEmployeesService(IEmployeeManager employeeManager, ICompanyManager companyManager)
        {
            _employeeManager = employeeManager;
            _companyManager = companyManager;
        }

        public async Task<int> CountEmployeesAsync(int companyId)
        {
            try
            {
                var employees = await _employeeManager.GetAll();
                var company = await _companyManager.Get(companyId);
                var employeesNumber = 0;

                if (company != null)
                {
                    employeesNumber = employees.Where(e => e.Company != null).Count(e => e.Company.Name == company.Name);
                }
                return employeesNumber;
            }
            catch
            {
                throw;
            }
        }
    }
}
