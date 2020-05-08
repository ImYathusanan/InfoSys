using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSys.DataAccess.Repository
{
    public class InsuranceRepositroy : IInsuranceRepository
    {
        private decimal _insuranceRate;

        private decimal _insuranceAmount;

        public decimal InsuranceContribution(decimal totalAmount)
        {
            if(totalAmount < 719)
            {
                _insuranceRate = .0m;
                _insuranceAmount = 0m;
            }
            else if(totalAmount >= 719 && totalAmount <= 4167)
            {
                _insuranceRate = 0.12m;
                _insuranceAmount = ((totalAmount - 719) * _insuranceRate);
            }
            else if(totalAmount > 4167)
            {
                _insuranceRate = 0.02m;
                _insuranceAmount = ((4167 - 719) * 0.12m) + ((totalAmount - 4167) * _insuranceRate);
            }

            return _insuranceAmount;
        }
    }
}
