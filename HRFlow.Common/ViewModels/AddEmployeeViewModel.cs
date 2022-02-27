using HRFlow.Common.AppConstants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRFlow.Common.ViewModels
{
    public class AddEmployeeViewModel
    {
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

        [Required]
        [MinLength(Const.IBANMinLength)]
        [MaxLength(Const.IBANMaxLength)]
        public string IBAN { get; set; }

        public DateTime HireDate { get; set; }

        public int DepartmentId { get; set; }

        public int JobId { get; set; }

        public decimal Salary { get; set; }

        public List<SelectListItem> LineManagers { get; } = new List<SelectListItem>();

        public List<SelectListItem> Departments { get; } = new List<SelectListItem>();

        public List<SelectListItem> Jobs { get; } = new List<SelectListItem>();
    }
}
