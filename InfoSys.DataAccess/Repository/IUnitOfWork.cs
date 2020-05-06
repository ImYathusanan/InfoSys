using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSys.DataAccess.Repository
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get; }

        void Complete();
    }
}
