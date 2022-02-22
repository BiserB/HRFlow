using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRFlow.Common.ViewModels
{
    public class DepartmentHistoryViewModel
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
