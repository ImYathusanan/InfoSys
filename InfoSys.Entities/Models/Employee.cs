using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace InfoSys.Entities.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string EmployeeNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public string Gender { get; set; }

        public string ImageUrl { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateJoined { get; set; }

        public string Designation { get; set; }

        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string InsuranceNumber { get; set; }

        public string PaymentMethod { get; set; }

        public string StudentLoan { get; set; }

        public string UnionMember { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string PostCode { get; set; }

        public IEnumerable<PaymentRecord> PaymentRecords { get; set; }
    }
}
