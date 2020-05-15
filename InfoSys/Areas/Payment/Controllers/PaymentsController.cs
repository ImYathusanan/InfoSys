using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoSys.DataAccess.Repository;
using InfoSys.Entities.Models;
using InfoSys.Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RotativaCore;

namespace InfoSys.Areas.Payment.Controllers
{
    [Area("Payment")]
    [Authorize(Roles = "Admin, Manager")]
    public class PaymentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private decimal overTimeHourse;
        private decimal contractualEarnings;
        private decimal overTimeEarnings;
        private decimal totalEarnings;
        private decimal tax;
        private decimal unionFee;
        private decimal studentLoan;
        private decimal insurance;
        private decimal totalDeduction;

        public PaymentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        public IActionResult Index()
        {
            var paymentRecords = _unitOfWork.Payments.GetAll().Select(p => new PaymenRecordIndexViewModel
            {
                Id = p.Id,
                EmployeeId = p.EmployeeId,
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                PaymentMonth = p.PaymentMonth,
                TaxYearId = p.TaxYearId,
                Year = _unitOfWork.TaxYears.GetTaxYearById(p.TaxYearId).YearOfTax,
              //  Year = _unitOfWork.TaxYears.GetTaxYearById(
                TotalEarnings = p.TotalEarnings,
                TotalDeduction = p.TotalDeduction,
                NetPayment = p.NetPayment,
                Employee = p.Employee
            });

            return View(paymentRecords);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.employees = _unitOfWork.Employees.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _unitOfWork.TaxYears.GetAll();
            var viewModel = new PaymentCreateViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(PaymentCreateViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var paymentRecord = new PaymentRecord()
                {
                    Id = viewModel.Id,
                    EmployeeId = viewModel.EmployeeId,
                    Firstname = _unitOfWork.Employees.Get(viewModel.EmployeeId).FirstName,
                    Lastname = _unitOfWork.Employees.Get(viewModel.EmployeeId).LastName,
                    PaymentDate = viewModel.PaymentDate,
                    PaymentMonth = viewModel.PaymentMonth,
                    TaxYearId = viewModel.TaxYearId,
                    TaxCode = viewModel.TaxCode,
                    HourlyRate = viewModel.HourlyRate,
                    HoursWorked = viewModel.HoursWorked,
                    ContractualHours = viewModel.ContractualHours,
                    OvertimeHourse = overTimeHourse = _unitOfWork.Payments.OverTimeHourse(viewModel.HoursWorked, viewModel.ContractualHours),
                    ContractualEarnings = contractualEarnings = _unitOfWork.Payments.ContractualEarnings(viewModel.ContractualEarnings, viewModel.HoursWorked, viewModel.HourlyRate),
                    OvertimeEarnings = overTimeEarnings = _unitOfWork.Payments.OverTimeEarnings(_unitOfWork.Payments.OverTimeRate(viewModel.HourlyRate), overTimeHourse),
                    TotalEarnings = totalEarnings = _unitOfWork.Payments.TotalEarnings(overTimeEarnings, contractualEarnings),
                    Tax = tax = _unitOfWork.Taxes.TaxAmount(totalEarnings),
                    UnionFee = unionFee = _unitOfWork.Employees.UnionFees(viewModel.EmployeeId),
                    StudentLoanCompany = studentLoan = _unitOfWork.Employees.StudentLoanPayment(viewModel.EmployeeId, totalEarnings),
                    InsuranceContribution = insurance = _unitOfWork.Insurences.InsuranceContribution(totalEarnings),
                    TotalDeduction = totalDeduction = _unitOfWork.Payments.TotalDeduction(tax, insurance, studentLoan, unionFee),
                    NetPayment = _unitOfWork.Payments.NetPay(totalEarnings, totalDeduction)
                };

                _unitOfWork.Payments.Create(paymentRecord);
                _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.employees = _unitOfWork.Employees.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _unitOfWork.TaxYears.GetAll();
            return View();
        }

        public IActionResult Detail(int id)
        {
            var paymentRecord = _unitOfWork.Payments.GetById(id);

            if (paymentRecord == null)
                return NotFound();

            var viewModel = new PaymentRecordDetailViewModel()
            {
                Id = paymentRecord.Id,
                EmployeeId = paymentRecord.EmployeeId,
                Firstname = paymentRecord.Firstname,
                Lastname = paymentRecord.Lastname,
                PaymentDate = paymentRecord.PaymentDate,
                PaymentMonth = paymentRecord.PaymentMonth,
                TaxYearId = paymentRecord.TaxYearId,
                Year = _unitOfWork.TaxYears.GetTaxYearById(paymentRecord.TaxYearId).YearOfTax,
                TaxCode = paymentRecord.TaxCode,
                HourlyRate = paymentRecord.HourlyRate,
                HoursWorked = paymentRecord.HoursWorked,
                ContractualHours = paymentRecord.ContractualHours,
                OvertimeHourse = paymentRecord.OvertimeHourse,
                OvarTimeRate = _unitOfWork.Payments.OverTimeRate(paymentRecord.HourlyRate),
                ContractualEarnings = paymentRecord.ContractualEarnings,
                OvertimeEarnings = paymentRecord.OvertimeEarnings,
                Tax = paymentRecord.Tax,
                InsuranceContribution = paymentRecord.InsuranceContribution,
                StudentLoanCompany = paymentRecord.StudentLoanCompany,
                TotalEarnings = paymentRecord.TotalEarnings,
                TotalDeduction = paymentRecord.TotalDeduction,
                Employee = paymentRecord.Employee,
                NetPayment = paymentRecord.NetPayment
            };

            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult PaymentSlip(int id)
        {
            var paymentRecord = _unitOfWork.Payments.GetById(id);

            if (paymentRecord == null)
                return NotFound();

            var viewModel = new PaymentRecordDetailViewModel()
            {
                Id = paymentRecord.Id,
                EmployeeId = paymentRecord.EmployeeId,
                Firstname = paymentRecord.Firstname,
                Lastname = paymentRecord.Lastname,
                PaymentDate = paymentRecord.PaymentDate,
                PaymentMonth = paymentRecord.PaymentMonth,
                TaxYearId = paymentRecord.TaxYearId,
                Year = _unitOfWork.TaxYears.GetTaxYearById(paymentRecord.TaxYearId).YearOfTax,
                TaxCode = paymentRecord.TaxCode,
                HourlyRate = paymentRecord.HourlyRate,
                HoursWorked = paymentRecord.HoursWorked,
                ContractualHours = paymentRecord.ContractualHours,
                OvertimeHourse = paymentRecord.OvertimeHourse,
                OvarTimeRate = _unitOfWork.Payments.OverTimeRate(paymentRecord.HourlyRate),
                ContractualEarnings = paymentRecord.ContractualEarnings,
                OvertimeEarnings = paymentRecord.OvertimeEarnings,
                Tax = paymentRecord.Tax,
                InsuranceContribution = paymentRecord.InsuranceContribution,
                StudentLoanCompany = paymentRecord.StudentLoanCompany,
                TotalEarnings = paymentRecord.TotalEarnings,
                TotalDeduction = paymentRecord.TotalDeduction,
                Employee = paymentRecord.Employee,
                NetPayment = paymentRecord.NetPayment
            };

            return View(viewModel);
        }

        public IActionResult GeneratePaymentslipPDF(int id)
        {
            var paymentSlip = new ActionAsPdf("PaymentSlip", new { id = id })
            {
                FileName = "PaymentSlip.pdf"
            };

            return paymentSlip;
        }
    }
}