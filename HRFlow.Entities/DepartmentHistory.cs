namespace HRFlow.Entities
{
    public class DepartmentHistory
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime LastModified { get; set; }
    }
}