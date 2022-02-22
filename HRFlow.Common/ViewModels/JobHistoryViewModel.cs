using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRFlow.Common.ViewModels
{
    public class JobHistoryViewModel
    {
        public int Id { get; set; }

        public int JobId { get; set; }

        public string JobTitle { get; set; }

        public decimal Salary { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
