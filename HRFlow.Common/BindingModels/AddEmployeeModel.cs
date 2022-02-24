using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRFlow.Common.BindingModels
{
    public class AddEmployeeModel : UpdateEmployeeModel
    {

        public DateTime HireDate { get; set; }

        public int DepartmentId { get; set; }

        public int JobId { get; set; }

        public int? LineManagerId { get; set; }
    }
}
