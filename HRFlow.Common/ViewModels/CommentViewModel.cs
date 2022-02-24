using HRFlow.Common.AppConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRFlow.Common.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [MinLength(Const.CommentMinLength)]
        [MaxLength(Const.CommentMaxLength)]
        public string Content { get; set; }

        public int EmployeeId { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string LastModified { get; set; }
    }
}
