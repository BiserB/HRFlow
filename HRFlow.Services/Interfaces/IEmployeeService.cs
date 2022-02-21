using HRFlow.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRFlow.Services.Interfaces
{
    public interface IEmployeeService
    {
        public bool AddEmployee();

        public IList<EmployeeViewModel> GetAllEmployees();
    }
}
