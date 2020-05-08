using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoSys.DataAccess.Repository;
using InfoSys.Entities.Models;
using InfoSys.Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InfoSys.Areas.Payment.Controllers
{
    [Area("Payment")]
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

        [HttpGet]
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
                Year = _unitOfWork.Payments.GetTaxYearById(p.TaxYearId).YearOfTax,
                TotalEarnings = p.TotalEarnings,
                TotalDeduction = p.TotalDeduction,
                NetPayment = p.NetPayment,
                Employee = p.Employee
            });

            return View(paymentRecords);
        }

        public IActionResult Create()
        {
            ViewBag.employees = _unitOfWork.Employees.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _unitOfWork.Payments.GetAllTaxYear();
            var viewModel = new PaymentCreateViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            ViewBag.taxYears = _unitOfWork.Payments.GetAllTaxYear();
            return View();
        }
    }
}