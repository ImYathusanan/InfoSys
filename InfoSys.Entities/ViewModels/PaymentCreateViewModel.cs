using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InfoSys.Entities.Models;

namespace InfoSys.Entities.ViewModels
{
    public class PaymentCreateViewModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string NiNo { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public string PaymentMonth { get; set; } = DateTime.Today.Month.ToString();

        public int TaxYearId { get; set; }

        public TaxYear TaxYear { get; set; }

        public string TaxCode { get; set; } = "1250L";

        public decimal HourlyRate { get; set; }

        public decimal HoursWorked { get; set; }

        public decimal ContractualHours { get; set; } = 144m;

        public decimal OvertimeHourse { get; set; }

        public decimal ContractualEarnings { get; set; }

        public decimal OvertimeEarnings { get; set; }

        public decimal Tax { get; set; }

        public decimal InsuranceContribution { get; set; }

        public decimal? UnionFee { get; set; }

        public decimal? StudentLoanCompany { get; set; }

        public decimal TotalEarnings { get; set; }

        public decimal TotalDeduction { get; set; }

        public decimal NetPayment { get; set; }
    }
}
