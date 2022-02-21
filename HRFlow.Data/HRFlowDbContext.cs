using HRFlow.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRFlow.Data
{
    public class HRFlowDbContext : DbContext
    {
        public HRFlowDbContext(DbContextOptions<HRFlowDbContext> options): base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
         
        public DbSet<Department> Departments { get; set; }
         
        public DbSet<Job> Jobs { get; set; }
         
        public DbSet<DepartmentHistory> DepartmentHistories { get; set; }
         
        public DbSet<JobHistory> JobHistories { get; set; }
         
        public DbSet<PaymentHistory> SalaryHistories { get; set; }

        public DbSet<Comment> Comments { get; set; }
         
        public DbSet<Contact> Contacts { get; set; }
         
        public DbSet<Municipality> Municipality { get; set; }
         
        public DbSet<Country> Countries { get; set; }
         
        public DbSet<Region> Regions { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.UseSerialColumns();

            mb.Entity<Employee>().HasKey(e => e.Id);
            mb.Entity<Employee>().Property(e => e.FirstName).HasColumnType("varchar(60)").IsRequired();
            mb.Entity<Employee>().Property(e => e.MiddleName).HasColumnType("varchar(60)").IsRequired();
            mb.Entity<Employee>().Property(e => e.LastName).HasColumnType("varchar(60)").IsRequired();
            mb.Entity<Employee>().Property(e => e.IBAN).HasColumnType("varchar(32)").IsRequired();

            mb.Entity<Employee>().HasOne(e => e.LineManager)
                                 .WithMany(e => e.Subordinates)
                                 .HasForeignKey(e => e.LineManagerId)
                                 .IsRequired(false)
                                 .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Employee>().HasOne(e => e.Contact)
                                 .WithOne(c => c.Employee)
                                 .HasForeignKey<Contact>(c => c.EmployeeId)
                                 .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Employee>().HasMany(e => e.DepartmentHistories)
                                 .WithOne(d => d.Employee)
                                 .HasForeignKey(d => d.EmployeeId)
                                 .IsRequired()
                                 .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Employee>().HasMany(e => e.JobHistories)
                                 .WithOne(d => d.Employee)
                                 .HasForeignKey(d => d.EmployeeId)
                                 .IsRequired()
                                 .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Employee>().HasMany(e => e.SalaryHistories)
                                 .WithOne(d => d.Employee)
                                 .HasForeignKey(d => d.EmployeeId)
                                 .IsRequired()
                                 .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Employee>().HasMany(e => e.CommentsReceived)
                                 .WithOne(d => d.Employee)
                                 .HasForeignKey(d => d.EmployeeId)
                                 .IsRequired()
                                 .OnDelete(DeleteBehavior.Restrict);

            //mb.Entity<Contact>().Navigation(c => c.Employee).IsRequired();
            mb.Entity<Contact>().Property(c => c.Address).HasColumnType("varchar(250)").IsRequired();
            mb.Entity<Contact>().Property(c => c.Address2).HasColumnType("varchar(250)");
            mb.Entity<Contact>().Property(c => c.PersonalEmail).HasColumnType("varchar(250)").IsRequired();
            mb.Entity<Contact>().Property(c => c.CompanyEmail).HasColumnType("varchar(250)").IsRequired();
            mb.Entity<Contact>().Property(c => c.PhoneNumber).HasColumnType("varchar(60)").IsRequired();
            mb.Entity<Contact>().Property(c => c.ExtraInfo).HasColumnType("varchar(250)");
            mb.Entity<Contact>().Property(c => c.Gender).HasConversion<string>().HasMaxLength(10);


            mb.Entity<Department>().HasKey(d => d.Id);
            mb.Entity<Department>().Property(d => d.Name).HasColumnType("varchar(250)").IsRequired();

            mb.Entity<DepartmentHistory>().HasKey(dh => dh.Id);
            mb.Entity<DepartmentHistory>().HasOne(dh => dh.Department)
                                          .WithMany(d => d.DepartmentHistories)
                                          .HasForeignKey(dh => dh.DepartmentId)
                                          .IsRequired()
                                          .OnDelete(DeleteBehavior.Restrict);


            mb.Entity<Job>().HasKey(j => j.Id);
            mb.Entity<Job>().Property(j => j.Title).HasColumnType("varchar(60)").IsRequired();
            mb.Entity<Job>().Property(j => j.Description).HasColumnType("varchar(250)").IsRequired();

            mb.Entity<JobHistory>().HasKey(jh => jh.Id);
            mb.Entity<JobHistory>().Property(jh => jh.Salary).HasColumnType("decimal(19,4)").IsRequired();
            mb.Entity<JobHistory>().Property(jh => jh.Description).HasColumnType("varchar(250)");
            mb.Entity<JobHistory>().HasOne(jh => jh.Job)
                                   .WithMany(j => j.JobHistories)
                                   .HasForeignKey(jh => jh.JobId)
                                   .IsRequired()
                                   .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<PaymentHistory>().HasKey(sh => sh.Id);
            mb.Entity<PaymentHistory>().Property(sh => sh.IBAN).HasColumnType("varchar(32)").IsRequired();
            mb.Entity<PaymentHistory>().Property(sh => sh.Amount).HasColumnType("decimal(19,4)").IsRequired();

            mb.Entity<Comment>().HasKey(c => c.Id);
            mb.Entity<Comment>().Property(c => c.Content).HasColumnType("varchar(250)").IsRequired();
            mb.Entity<Comment>().HasOne(c => c.Author)
                                .WithMany(e => e.CommentsLeft)
                                .HasForeignKey(c => c.AuthorId)
                                .IsRequired()
                                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
