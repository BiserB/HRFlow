using HRFlow.Common.AppConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRFlow.Common.ViewModels
{
    public class EmployeeDetailsViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(Const.EmplNameMinLength)]
        [MaxLength(Const.EmplNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(Const.EmplNameMinLength)]
        [MaxLength(Const.EmplNameMaxLength)]
        public string MiddleName { get; set; }

        [Required]
        [MinLength(Const.EmplNameMinLength)]
        [MaxLength(Const.EmplNameMaxLength)]
        public string LastName { get; set; }

        public int? LineManagerId { get; set; }

        public string LineManagerName { get; set; }

        public string HireDate { get; set; }

        [Required]
        [MinLength(Const.IBANMinLength)]
        [MaxLength(Const.IBANMaxLength)]
        public string IBAN { get; set; }

        public decimal Salary { get; set; }

        [Required]
        [MinLength(Const.CommentMinLength)]
        [MaxLength(Const.CommentMaxLength)]
        public string Comment { get; set; }

        public List<DepartmentHistoryViewModel> Departments { get; set; } = new List<DepartmentHistoryViewModel>();

        public List<JobHistoryViewModel> Jobs { get; set; } = new List<JobHistoryViewModel>();

        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();

        public decimal[] PaymentHistory { get; set; } = new decimal[0];

        public DateTime? TerminationDate { get; set; }
    }
}
