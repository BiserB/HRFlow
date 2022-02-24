using HRFlow.Common.AppConstants;
using System.ComponentModel.DataAnnotations;

namespace HRFlow.Common.BindingModels
{
    public class UpdateEmployeeModel
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

        [Required]
        [MinLength(Const.IBANMinLength)]
        [MaxLength(Const.IBANMaxLength)]
        public string IBAN { get; set; }

        public decimal Salary { get; set; }
    }
}
