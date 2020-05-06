using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InfoSys.Entities.Models
{
    public class PaymentRecord
    {
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }


        [MaxLength(50)]
        public string Firstname { get; set; }

        [MaxLength(50)]
        public string Lastname { get; set; }

        public string NiNo { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMonth { get; set; }

        [ForeignKey("TaxYear")]
        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; }

        public string TaxCode { get; set; }


        [Column(TypeName = "money")]
        public decimal HourlyRate { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal HoursWorked { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal ContractualHours { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal OvertimeHourse { get; set; }


        [Column(TypeName = "money")]
        public decimal ContractualEarnings { get; set; }


        [Column(TypeName = "money")]
        public decimal OvertimeEarnings { get; set; }


        [Column(TypeName = "money")]
        public decimal Tax { get; set; }


        [Column(TypeName = "money")]
        public decimal InsuranceContribution { get; set; }


        [Column(TypeName = "money")]
        public decimal? UnionFee { get; set; }


        [Column(TypeName = "money")]
        public decimal? StudentLoanCompany { get; set; }


        [Column(TypeName = "money")]
        public decimal TotalEarnings { get; set; }


        [Column(TypeName = "money")]
        public decimal TotalDeduction { get; set; }


        [Column(TypeName = "money")]
        public decimal NetPayment { get; set; }
    }
}
