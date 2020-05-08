using InfoSys.Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSys.DataAccess.Repository
{
    public interface IPaymentRepository
    {
        void Create(PaymentRecord payemtRecord);

        PaymentRecord GetById(int id);

        TaxYear GetTaxYearById(int id);

        IEnumerable<PaymentRecord> GetAll();

        IEnumerable<SelectListItem> GetAllTaxYear();

        decimal OverTimeHourse(decimal hourseWorked, decimal contractualHourse);

        decimal ContractualEarnings(decimal contractualHourse, decimal hourseWorkded, decimal hourlyRate);

        decimal OverTimeRate(decimal hourlyRate);

        decimal OverTimeEarnings(decimal overtimeRate, decimal overtimeHourse);

        decimal TotalEarnings(decimal overtimeEarnings, decimal contratualEarnings);

        decimal TotalDeduction(decimal tax, decimal insurance, decimal studentLoad, decimal unionFee);

        decimal NetPay(decimal totalEarnings, decimal totalDeduction);
    }
}
