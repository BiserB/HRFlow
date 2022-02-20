namespace HRFlow.Entities
{
    public class Job
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<JobHistory> JobHistories { get; set; }
    }
}
