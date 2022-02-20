namespace HRFlow.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<DepartmentHistory> DepartmentHistories { get; set; }
    }
}
