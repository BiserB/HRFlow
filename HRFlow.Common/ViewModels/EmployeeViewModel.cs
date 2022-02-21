using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRFlow.Common.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        //public int? LineManagerId { get; set; }

        public string LineManagerName { get; set; }

        public string HireDate { get; set; }

        public string LongTermEmployee { get; set; }
    }
}
