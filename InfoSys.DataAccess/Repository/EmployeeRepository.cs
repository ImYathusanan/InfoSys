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
            throw new NotImplementedException();
        }

        public decimal UnionFees(int id)
        {
            throw new NotImplementedException();
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
