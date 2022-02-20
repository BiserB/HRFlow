using System.ComponentModel.DataAnnotations;

namespace HRFlow.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public int AuthorId { get; set; }

        public Employee Author { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsDeleted { get; set; }
    }
}
