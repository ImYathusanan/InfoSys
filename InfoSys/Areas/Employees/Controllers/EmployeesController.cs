using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoSys.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using InfoSys.Entities.Models;
using InfoSys.Entities.ViewModels;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;

namespace InfoSys.Areas.Employees.Controllers
{
    [Area("Employee")]
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvronment;

        public EmployeesController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvronment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _unitOfWork.Employees.GetAll();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new EmployeeFormViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            var employee = new Employee
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Gender = viewModel.Gender,
                DateOfBirth = viewModel.DateOfBirth,
                DateJoined = viewModel.DateJoined,
                InsuranceNumber = viewModel.InsuranceNumber,
                PaymentMethod = viewModel.PaymentMethod,
                StudentLoan = viewModel.StudentLoan,
                UnionMember = viewModel.UnionMember,
                Address = viewModel.Address,
                Phone = viewModel.Phone,
                City = viewModel.City,
                PostCode = viewModel.PostCode,
                Designation = viewModel.Designation
            };

            if(viewModel.ImageUrl != null && viewModel.ImageUrl.Length > 0)
            {
                var uploadDirectory = @"images/employees";
                var fileName = Path.GetFileNameWithoutExtension(viewModel.ImageUrl.FileName);
                var fileExtention = Path.GetExtension(viewModel.ImageUrl.FileName);
                var webRootPath = _hostingEnvronment.WebRootPath;
                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + fileExtention;
                var path = Path.Combine(webRootPath, uploadDirectory, fileName);
                await viewModel.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));

                employee.ImageUrl = "/" + uploadDirectory + "/" + fileName;
            }

            await _unitOfWork.Employees.Add(employee);
                  _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Edit(int id )
        {
            var employee = _unitOfWork.Employees.Get(id);

            if (employee == null)
                return NotFound();

            var viewModel = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Gender = employee.Gender,
                DateOfBirth = employee.DateOfBirth,
                DateJoined = employee.DateJoined,
                InsuranceNumber = employee.InsuranceNumber,
                PaymentMethod = employee.PaymentMethod,
                StudentLoan = employee.StudentLoan,
                UnionMember = employee.UnionMember,
                Address = employee.Address,
                Phone = employee.Phone,
                City = employee.City,
                PostCode = employee.PostCode,
                Designation = employee.Designation
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            var employee = _unitOfWork.Employees.Get(viewModel.Id);

            if (employee == null)
                return NotFound();

            employee.EmployeeNumber = viewModel.EmployeeNumber;
            employee.FirstName = viewModel.FirstName;
            employee.LastName = viewModel.LastName;
            employee.InsuranceNumber = viewModel.InsuranceNumber;
            employee.Gender = viewModel.Gender;
            employee.Email = viewModel.Email;
            employee.DateOfBirth = viewModel.DateOfBirth;
            employee.DateJoined = viewModel.DateJoined;
            employee.Phone = viewModel.Phone;
            employee.Designation = viewModel.Designation;
            employee.PaymentMethod = viewModel.PaymentMethod;
            employee.StudentLoan = viewModel.StudentLoan;
            employee.UnionMember = viewModel.UnionMember;
            employee.Address = viewModel.Address;
            employee.City = viewModel.City;
            employee.PostCode = viewModel.PostCode;
            if(viewModel.ImageUrl != null && viewModel.ImageUrl.Length > 0)
            {
                var uploadDirectory = @"images/employees";
                var fileName = Path.GetFileNameWithoutExtension(viewModel.ImageUrl.FileName);
                var fileExtention = Path.GetExtension(viewModel.ImageUrl.FileName);
                var webRootPath = _hostingEnvronment.WebRootPath;
                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + fileExtention;
                var path = Path.Combine(webRootPath, uploadDirectory, fileName);
                await viewModel.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));

                employee.ImageUrl = "/" + uploadDirectory + "/" + fileName;
            }

            await _unitOfWork.Employees.UpdateEmployee(employee);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }


    }
}