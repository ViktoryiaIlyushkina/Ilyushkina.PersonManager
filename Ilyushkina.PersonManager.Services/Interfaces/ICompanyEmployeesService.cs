﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyushkina.PersonManager.Services.Interfaces
{
    public interface ICompanyEmployeesService
    {
        public Task<int> CountEmployeesAsync(int companyId);

    }
}
