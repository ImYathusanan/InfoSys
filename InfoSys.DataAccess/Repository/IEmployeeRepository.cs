using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InfoSys.Entities.Models;

namespace InfoSys.DataAccess.Repository
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);

        Employee Get(int id);
        IEnumerable<Employee> GetAll();

        void Delete(int id);

        decimal UnionFees(int id);
        decimal StudentLoanPayment(int id, decimal totalAmount);

        void UpdateEmployee(int id);
        void UpdateEmployee(Employee employee);
    }
}
