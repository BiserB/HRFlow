using HRFlow.App.Models;
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

            if (employeeModel == null)
            {
                return RedirectToAction(nameof(Error));
            }

            return View(employeeModel);
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
                return RedirectToAction(nameof(EmployeeDetails), new { id = model.Id } );
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}