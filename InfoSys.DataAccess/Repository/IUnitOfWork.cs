using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfoSys.DataAccess.Repository
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get; }

        IPaymentRepository Payments { get; }

        ITaxRepository Taxes { get; }

        IInsuranceRepository Insurences { get; }

        void Complete();
    }
}
