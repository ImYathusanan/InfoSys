using InfoSys.DataAccess.Data;
using InfoSys.Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoSys.DataAccess.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        private decimal _contractualEarnings;

        private decimal _overtimeHourse;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal ContractualEarnings(decimal contractualHourse, decimal hourseWorkded, decimal hourlyRate)
        {
            if (hourseWorkded < contractualHourse)
                _contractualEarnings = hourseWorkded * hourlyRate;
            else
                _contractualEarnings = contractualHourse * hourlyRate;

            return _contractualEarnings;
        }

        public void Create(PaymentRecord payemtRecord)
        {
            _context.PaymentRecords.Add(payemtRecord);
        }

        public IEnumerable<PaymentRecord> GetAll() => _context
                                                        .PaymentRecords
                                                        .OrderBy(p => p.EmployeeId)
                                                        .ToList();

        public IEnumerable<SelectListItem> GetAllTaxYear()
        {
            var allTaxYear = _context.TaxYears.Select(taxYear => new SelectListItem
            {
                Text = taxYear.YearOfTax,
                Value = taxYear.ToString()
            });

            return allTaxYear;
        }

        public PaymentRecord GetById(int id) => _context.PaymentRecords
                                                .Where(p => p.Id == id)
                                                .FirstOrDefault();

        public decimal NetPay(decimal totalEarnings, decimal totalDeduction) => totalEarnings - totalDeduction;


        public decimal OverTimeEarnings(decimal overtimeRate, decimal overtimeHourse) => overtimeHourse * overtimeRate;
       

        public decimal OverTimeHourse(decimal hourseWorked, decimal contractualHourse)
        {
            if (hourseWorked <= contractualHourse)
                _overtimeHourse = 0.00m;
            else if (hourseWorked > contractualHourse)
                _overtimeHourse = hourseWorked - contractualHourse;

            return _overtimeHourse;
        }

        public decimal OverTimeRate(decimal hourlyRate) => hourlyRate * 1.5m;

        public decimal TotalDeduction(decimal tax, decimal insurance, decimal studentLoad, decimal unionFee)
         => tax + insurance + studentLoad + unionFee;

        public decimal TotalEarnings(decimal overtimeEarnings, decimal contratualEarnings)
         => overtimeEarnings + contratualEarnings;
    }
}
