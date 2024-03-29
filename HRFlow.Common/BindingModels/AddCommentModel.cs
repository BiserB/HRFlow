﻿using HRFlow.Common.AppConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRFlow.Common.BindingModels
{
    public class AddCommentModel
    {
        public int EmployeeId { get; set; }

        [Required]
        [MinLength(Const.CommentMinLength)]
        [MaxLength(Const.CommentMaxLength)]
        public string Comment { get; set; }
    }
}
