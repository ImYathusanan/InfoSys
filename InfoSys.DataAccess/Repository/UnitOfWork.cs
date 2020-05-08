using InfoSys.DataAccess.Data;
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

        public IPaymentRepository Payments { get; private set; }

        public ITaxRepository Taxes { get; private set; }

        public IInsuranceRepository Insurances { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Employees = new EmployeeRepository(context);
            Payments = new PaymentRepository(context);
            Taxes = new TaxRepository(context);
            Insurances = new InsuranceRepositroy(context);
        }

        public void Complete()
        {
           _context.SaveChanges();
        }
    }
}
