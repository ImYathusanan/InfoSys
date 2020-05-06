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
        public async Task Add(Employee employee)
        {
            await _context.Employees.AddAsync(employee);

        }

        public Employee Get(int id) =>
            _context.Employees.Where(e => e.Id == id)
            .FirstOrDefault();

        public async Task Delete(int id)
        {
            var employee = Get(id);
             _context.Remove(employee);
        }



        public IEnumerable<Employee> GetAll() => _context.Employees;

        public decimal StudentLoanPayment(int id, decimal totalAmount)
        {
            throw new NotImplementedException();
        }

        public decimal UnionFees(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateEmployee(int id)
        {
            var employee = Get(id);
            _context.Employees.Update(employee);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            _context.Update(employee);
        }
    }
}
