using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRFlow.Entities
{
    public class SalaryHistory
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public string IBAN { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
