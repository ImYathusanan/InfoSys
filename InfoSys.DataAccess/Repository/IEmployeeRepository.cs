using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InfoSys.Entities.Models;

namespace InfoSys.DataAccess.Repository
{
    public interface IEmployeeRepository
    {
        Task Add(Employee employee);

        Employee Get(int id);
        IEnumerable<Employee> GetAll();

        Task Delete(int id);

        decimal UnionFees(int id);
        decimal StudentLoanPayment(int id, decimal totalAmount);

        Task UpdateEmployee(int id);
        Task UpdateEmployee(Employee employee);
    }
}
