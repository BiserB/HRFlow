using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRFlow.Common.ViewModels
{
    public class PaymentHistoryViewModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string IBAN { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
