using HRFlow.Common.AppConstants;
using System.ComponentModel.DataAnnotations;

namespace HRFlow.Common.BindingModels
{
    public class UpdateCommentModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        [Required]
        [MinLength(Const.CommentMinLength)]
        [MaxLength(Const.CommentMaxLength)]
        public string Content { get; set; }
    }
}
