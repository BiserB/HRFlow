using HRFlow.App.Models;
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
            //var emplyeeModels = employeeService.GetAllEmployees();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GetEmployees()
        {
            var emplyeeModels = employeeService.GetAllEmployees();

            return new JsonResult(emplyeeModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}