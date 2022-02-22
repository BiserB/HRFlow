using HRFlow.Common.BindingModels;
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
        bool AddEmployee();

        IList<EmployeeViewModel> GetAllEmployees(bool onlyActiveEmployees);

        EmployeeDetailsViewModel GetEmployee(int id);

        bool UpdateEmployee(UpdateEmployeeModel model);

        bool AddComment(AddCommentModel model);
    }
}
