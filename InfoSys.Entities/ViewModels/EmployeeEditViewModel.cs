using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using InfoSys.Entities.Models;
using Microsoft.AspNetCore.Http;

namespace InfoSys.Entities.ViewModels
{
    public class EmployeeEditViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string EmployeeNumber { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public string Gender { get; set; }

        [Required]
        [Display(Name = "Profile Picture")]
        public IFormFile ImageUrl { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Joined")]
        public DateTime DateJoined { get; set; }

        public string Designation { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string InsuranceNumber { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public StudentLoan StudentLoan { get; set; }

        public UnionMember UnionMember { get; set; }

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
    }
}
