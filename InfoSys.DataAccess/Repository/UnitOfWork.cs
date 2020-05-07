﻿using InfoSys.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfoSys.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IEmployeeRepository Employees { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Employees = new EmployeeRepository(context);
        }

        public void Complete()
        {
           _context.SaveChanges();
        }
    }
}
