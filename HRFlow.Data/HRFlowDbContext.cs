using HRFlow.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRFlow.Data
{
    public class HRFlowDbContext : DbContext
    {
        public HRFlowDbContext(DbContextOptions<HRFlowDbContext> options): base(options)
        {

        }

        DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
    }
}
