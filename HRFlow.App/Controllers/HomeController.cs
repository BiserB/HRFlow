﻿using HRFlow.App.Models;
using HRFlow.Common.BindingModels;
using HRFlow.Common.ViewModels;
using HRFlow.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace HRFlow.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IEmployeeService employeeService;

        public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService)
        {
            this.logger = logger;
            this.employeeService = employeeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult EmployeeDetails(int id)
        {
            var employeeModel = employeeService.GetEmployee(id);

            return View(employeeModel ?? new EmployeeDetailsViewModel());
        }

        public IActionResult GetEmployees(bool onlyActiveEmployees = true)
        {
            var employeeModels = employeeService.GetAllEmployees(onlyActiveEmployees);

            return new JsonResult(employeeModels);
        }

        public IActionResult GetEmployee(int id)
        {
            var employeeModel = employeeService.GetEmployee(id);

            return new JsonResult(employeeModel);
        }

        [HttpPost]
        public IActionResult UpdateEmployee(UpdateEmployeeModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(EmployeeDetails), new { id = model.Id });
            }

            var isUpdated = employeeService.UpdateEmployee(model);

            return RedirectToAction(nameof(EmployeeDetails), new { id = model.Id });
        }

        [HttpPost]
        public IActionResult AddComment(AddCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(EmployeeDetails), new { id = model.EmployeeId });
            }

            var isAdded = employeeService.AddComment(model);

            return RedirectToAction(nameof(EmployeeDetails), new { id = model.EmployeeId });
        }

        [HttpPost]
        public IActionResult UpdateComment(UpdateCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult("invalid model");
            }

            var isUpdated = employeeService.UpdateComment(model);

            if (isUpdated)
            {
                return new JsonResult("ok");
            }

            return new JsonResult("invalid model");
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            var model = employeeService.GetEmployeeModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var employeeId = employeeService.AddEmployee(model);

            if (employeeId == 0)
            {
                return View(model);
            }

            return RedirectToAction(nameof(EmployeeDetails), new { id = employeeId });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}