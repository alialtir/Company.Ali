using Company.Ali.BLL.Interfaces;
using Company.Ali.BLL.Repositories;
using Company.Ali.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Ali.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _context;

        public IDepartmentRepository DepartmentRepository {  get; } // NULL

        public IEmployeeRepository EmployeeRepository { get; } // NULL

        public UnitOfWork(CompanyDbContext context)
        {
            _context = context;

            DepartmentRepository = new DepartmentRepository(_context);

            EmployeeRepository = new EmployeeRepository(_context);

         
        }
    }
}
