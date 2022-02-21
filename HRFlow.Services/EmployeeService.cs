using HRFlow.Common.ViewModels;
using HRFlow.Data;
using HRFlow.Services.Interfaces;

namespace HRFlow.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HRFlowDbContext dbContext;

        public EmployeeService(HRFlowDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddEmployee()
        {
            return dbContext.SaveChanges() > 0;            
        }

        public IList<EmployeeViewModel> GetAllEmployees()
        {
            var today = DateTime.Today;

            var employees = dbContext.Employees
                .Select(e => new EmployeeViewModel()
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    MiddleName = e.MiddleName,
                    LastName = e.LastName,
                    LineManagerName = e.LineManager != null ? e.LineManager.LastName : String.Empty,
                    HireDate = e.HireDate.ToShortDateString(),
                    LongTermEmployee = (today.Date - e.HireDate.Date).Days.ToString()
                })
                .ToList();

            return employees;
        }
    }
}