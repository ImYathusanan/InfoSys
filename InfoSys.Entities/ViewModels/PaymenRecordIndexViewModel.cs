using InfoSys.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace InfoSys.Entities.ViewModels
{
    public class PaymenRecordIndexViewModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        public string PaymentMonth { get; set; }

        public int TaxYearId { get; set; }

        public string Year { get; set; }

        public decimal TotalEarnings { get; set; }

        public decimal TotalDeduction { get; set; }

        public decimal NetPayment { get; set; }
    }
}
