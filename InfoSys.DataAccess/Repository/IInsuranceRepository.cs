using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSys.DataAccess.Repository
{
    public interface IInsuranceRepository
    {
        decimal InsuranceContribution(decimal totalAmount);
    }
}
