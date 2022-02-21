namespace HRFlow.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int? LineManagerId { get; set; }

        public Employee LineManager { get; set; }

        public string IBAN { get; set; }

        public Contact Contact { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public DateTime LastModified { get; set; }

        public List<Employee> Subordinates { get; set; }

        public List<DepartmentHistory> DepartmentHistories { get; set; }

        public List<JobHistory> JobHistories { get; set; }

        public List<PaymentHistory> SalaryHistories { get; set; }
        
        public List<Comment> CommentsReceived { get; set; }

        public List<Comment> CommentsLeft { get; set; }
    }
}