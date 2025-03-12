using Company.Ali.BLL.Interfaces;
using Company.Ali.DAL.Data.Contexts;
using Company.Ali.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Ali.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CompanyDbContext context) : base(context) // ASk CLR Create Object From CompanyDbContext
        {
            
        }



    }
}
