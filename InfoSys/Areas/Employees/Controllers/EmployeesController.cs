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
using Microsoft.AspNetCore.Authorization;
using InfoSys.Utils;

namespace InfoSys.Areas.Employees.Controllers
{
    [Area("Employees")]
    [Authorize]
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
        public IActionResult Create(EmployeeFormViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    EmployeeNumber = viewModel.EmployeeNumber,
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
                    Email = viewModel.Email,
                    City = viewModel.City,
                    PostCode = viewModel.PostCode,
                    Designation = viewModel.Designation
                };

                if (viewModel.ImageUrl != null && viewModel.ImageUrl.Length > 0)
                {
                    var uploadDirectory = @"images/employees";
                    var fileName = Path.GetFileNameWithoutExtension(viewModel.ImageUrl.FileName);
                    var fileExtention = Path.GetExtension(viewModel.ImageUrl.FileName);
                    var webRootPath = _hostingEnvronment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + fileExtention;
                    var path = Path.Combine(webRootPath, uploadDirectory, fileName);
                    viewModel.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));

                    employee.ImageUrl = "/" + uploadDirectory + "/" + fileName;
                }

                 _unitOfWork.Employees.Add(employee);
                 _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
        }

      return View();
    }

        
        public IActionResult Edit(int id )
        {
            var employee = _unitOfWork.Employees.Get(id);

            if (employee == null)
                return NotFound();

            var viewModel = new EmployeeEditViewModel()
            {
                EmployeeNumber = employee.EmployeeNumber,
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
                Email = employee.Email,
                City = employee.City,
                PostCode = employee.PostCode,
                Designation = employee.Designation
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeEditViewModel viewModel)
        {
            var employee = _unitOfWork.Employees.Get(viewModel.Id);

            if (employee == null)
                return NotFound();

            if(ModelState.IsValid)
            {

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
                if (viewModel.ImageUrl != null && viewModel.ImageUrl.Length > 0)
                {
                    var uploadDirectory = @"images/employees";
                    var fileName = Path.GetFileNameWithoutExtension(viewModel.ImageUrl.FileName);
                    var fileExtention = Path.GetExtension(viewModel.ImageUrl.FileName);
                    var webRootPath = _hostingEnvronment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + fileExtention;
                    var path = Path.Combine(webRootPath, uploadDirectory, fileName);
                    viewModel.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));

                    employee.ImageUrl = "/" + uploadDirectory + "/" + fileName;
                }

                _unitOfWork.Employees.UpdateEmployee(employee);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var employee = _unitOfWork.Employees.Get(id);

            if (employee == null)
                return NotFound();

            var viewModel = new EmployeeDetailViewModel()
            {
                Id = employee.Id,
                EmployeeNumber = employee.EmployeeNumber,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Gender = employee.Gender,
                DateOfBirth = employee.DateOfBirth,
                DateJoined = employee.DateJoined,
                Designation = employee.Designation,
                InsuranceNumber = employee.InsuranceNumber,
                Email = employee.Email,
                Phone = employee.Phone,
                PaymentMethod = employee.PaymentMethod,
                UnionMember = employee.UnionMember,
                StudentLoan = employee.StudentLoan,
                Address = employee.Address,
                City = employee.City,
                ImageUrl = employee.ImageUrl,
                PostCode = employee.PostCode
            };

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _unitOfWork.Employees.Get(id);

            if (employee == null)
                return NotFound();

            var viewModel = new EmployeeDeleteViewModel()
            {
                Id = employee.Id,
                Firstname = employee.FirstName,
                Lastname = employee.LastName
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(EmployeeDeleteViewModel employee)
        {
             _unitOfWork.Employees.Delete(employee.Id);
            _unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }


    }
}