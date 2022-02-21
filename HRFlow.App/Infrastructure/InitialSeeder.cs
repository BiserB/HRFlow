using HRFlow.Data;
using HRFlow.Entities;
using HRFlow.Entities.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HRFlow.App.Infrastructure
{
    public static class InitialSeeder
    {
        public static void Seed(this IHost host)
        {
            using var serviceScope = host.Services.CreateScope();

            using var dbContext = serviceScope.ServiceProvider.GetRequiredService<HRFlowDbContext>();

            dbContext.Database.EnsureCreated();

            AddEntities(dbContext);
        }

        private static void AddEntities(HRFlowDbContext dbContext)
        {
            var hasEmployees = dbContext.Employees.Any();

            if (hasEmployees)
            {
                return;
            }

            var employee = new Employee()
            {
                FirstName = "Boss",
                MiddleName = "O",
                LastName = "Boss",
                IBAN = "213123123",
                HireDate = DateTime.Now.AddYears(-3),
                LastModified = DateTime.Now,
            };

            var contact = new Contact()
            {
                Employee = employee,
                Address = "Sofia, 7484 Roundtree Drive",
                CompanyEmail = "boss@enterprise.com",
                PersonalEmail = "persona@mail.bg",
                PhoneNumber = "05553439394",
                Gender = Gender.Male,
                BirthDate = DateTime.Now.AddYears(-45),
            };

            var employee1 = new Employee()
            {
                FirstName = "Terri",
                MiddleName = "Lee",
                LastName = "Goldberg",
                IBAN = "880391798097811",
                HireDate = DateTime.Now.AddYears(-2),
                LastModified = DateTime.Now,
                LineManager = employee
            }; 

            var contact1 = new Contact()
            {
                Employee = employee1,
                Address = "Boston, 6387 Scenic Avenue",
                CompanyEmail = "terri0@adventure-works.com",
                PersonalEmail = "persona@mail.bg",
                PhoneNumber = "03533439394",
                Gender = Gender.Male,
                BirthDate = DateTime.Now.AddYears(-35),
            };

            dbContext.Employees.Add(employee);
            dbContext.Contacts.Add(contact);

            dbContext.Employees.Add(employee1);
            dbContext.Contacts.Add(contact1);

            dbContext.SaveChanges();

            var job = new Job()
            {
                Title = "Chief Executive Officer",
                Description = "Chief Executive Officer"
            };

            var jobHistory = new JobHistory()
            {
                Employee = employee,
                Job = job,
                Salary = 12000,
                StartDate = employee.HireDate
            };

            var job1 = new Job()
            {
                Title = "Tool Designer",
                Description = "Designing tools"
            };

            var jobHistory1 = new JobHistory()
            {
                Employee = employee1,
                Job = job1,
                Salary = 4000,
                StartDate = employee1.HireDate,
                EndDate = employee1.HireDate.AddYears(1)
            };

            var jobHistory2 = new JobHistory()
            {
                Employee = employee1,
                Job = job1,
                Salary = 6000,
                StartDate = employee1.HireDate.AddYears(1)
            };

            dbContext.Jobs.Add(job);
            dbContext.JobHistories.Add(jobHistory);

            dbContext.Jobs.Add(job1);
            dbContext.JobHistories.Add(jobHistory1);
            dbContext.JobHistories.Add(jobHistory2);

            dbContext.SaveChanges();

            var now = DateTime.Now;
            var employeePaymentMonts = (now.Year * 12 + now.Month) - (employee.HireDate.Year * 12 + employee.HireDate.Month);

            for (int i = 1; i <= employeePaymentMonts; i++)
            {
                dbContext.SalaryHistories.Add(new PaymentHistory()
                {
                    Employee = employee,
                    IBAN = employee.IBAN,
                    Amount = 12000,
                    PaymentDate = employee.HireDate.AddMonths(i),
                });
            }

            dbContext.SaveChanges();

            var employee1PaymentMonts = (now.Year * 12 + now.Month) - (employee1.HireDate.Year * 12 + employee1.HireDate.Month);

            Func<DateTime, DateTime, DateTime?, bool> paymentDateIsValid = (paymentDate, startDate, endDate) =>
            {
                if (endDate == null)
                {
                    return paymentDate >= startDate;
                }

                return startDate <= paymentDate && paymentDate <= endDate;
            };

            for (int i = 1; i <= employee1PaymentMonts; i++)
            {
                var paymentDate = employee1.HireDate.AddMonths(i);

                var salary = employee1.JobHistories.FirstOrDefault(jh => paymentDateIsValid(paymentDate, jh.StartDate, jh.EndDate))?.Salary;

                dbContext.SalaryHistories.Add(new PaymentHistory()
                {
                    Employee = employee1,
                    IBAN = employee1.IBAN,
                    Amount = salary ?? 0,
                    PaymentDate = paymentDate,
                });
            }

            dbContext.SaveChanges();
        }

        private static void AddSeedEntities(HRFlowDbContext dbContext)
        {
            var hasEmployees = dbContext.Employees.Any();

            if (hasEmployees)
            {
                return;
            }

            var entityNames = new string[]{ "Employee", "Contact", "Department", "DepartmentHistory", "Job", "JobHistory" };

            foreach (var entityName in entityNames)
            {
                var entitiesToAdd = GetSeedData(entityName);

                dbContext.AddRange(entitiesToAdd);

                dbContext.SaveChanges();
            }         
        }

        private static object[] GetSeedData(string fileName)
        {
            var directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var basePath = Path.GetFullPath(Path.Combine(directoryPath!, @"..\..\..\.."));

            string fullPath = Path.Combine(basePath, @$"HRFlow.Seed\{fileName}.csv");

            string[] lines = File.ReadAllLines(fullPath);

            return lines;
        }
    }
}
