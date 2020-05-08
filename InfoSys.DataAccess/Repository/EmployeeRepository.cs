using InfoSys.DataAccess.Data;
using InfoSys.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSys.DataAccess.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        private decimal _studentLoanAmount;

        private decimal _unionFee;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Employee employee)
        {
             _context.Employees.Add(employee);

        }

        public Employee Get(int id) =>
            _context.Employees.Where(e => e.Id == id)
            .FirstOrDefault();

        public void Delete(int id)
        {
            var employee = Get(id);
             _context.Remove(employee);
        }



        public IEnumerable<Employee> GetAll()
        {
           return _context.Employees.ToList();
        }

        public decimal StudentLoanPayment(int id, decimal totalAmount)
        {
            var employee = Get(id);

            if (employee.StudentLoan == StudentLoan.Yes && totalAmount > 1750 && totalAmount < 2000)
                _studentLoanAmount = 15m;
            else if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2000 && totalAmount < 2250)
                _studentLoanAmount = 38m;
            else if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2250 && totalAmount < 2500)
                _studentLoanAmount = 60m;
            else if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2500)
                _studentLoanAmount = 83m;
            else
                _studentLoanAmount = 0m;

            return _studentLoanAmount;
        }

        public decimal UnionFees(int id)
        {
            var employee = Get(id);

            _unionFee = employee.UnionMember == UnionMember.Yes ? 10m : 0m;

            return _unionFee;
        }

        public void UpdateEmployee(int id)
        {
            var employee = Get(id);
            _context.Employees.Update(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Update(employee);
        }
    }
}
