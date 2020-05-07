using InfoSys.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSys.Entities.ViewModels
{
    public class EmployeeDetailViewModel
    {
        public int Id { get; set; }

        public string EmployeeNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string ImageUrl { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateJoined { get; set; }

        public string Designation { get; set; }

        public string Email { get; set; }

        public string InsuranceNumber { get; set; }

        public string PaymentMethod { get; set; }

        public string StudentLoan { get; set; }

        public string UnionMember { get; set; }

        public string Address { get; set; }

        public int Phone { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }
    }
}
