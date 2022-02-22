using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRFlow.Common.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int EmployeeId { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string LastModified { get; set; }
    }
}
