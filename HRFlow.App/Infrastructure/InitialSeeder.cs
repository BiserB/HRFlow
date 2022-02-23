using HRFlow.Data;
using HRFlow.Entities;
using HRFlow.Entities.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
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

            if (dbContext.Employees.Any())
            {
                return;
            }

            AddRegions(dbContext);
            AddCountries(dbContext);
            AddEmployees(dbContext);
            AddContacts(dbContext);
            AddDepartments(dbContext);
            AddJobs(dbContext);
            AddJobHistories(dbContext);
            AddDepartmentHistories(dbContext);
        }

       

        private static void AddRegions(HRFlowDbContext dbContext)
        {
            var regionNames = GetSeedData("Regions");

            foreach (var regionName in regionNames)
            {
                dbContext.Regions.Add(new Region()
                {
                    Name = regionName
                });
            }

            dbContext.SaveChanges();
        }

        private static void AddCountries(HRFlowDbContext dbContext)
        {
            var countries = GetSeedData("world-regions-according-to-the-world-bank");

            var regions = dbContext.Regions.ToList();

            foreach (var country in countries.Skip(1))
            {
                var c = country.Split(',');

                dbContext.Countries.Add(new Country()
                {
                    Name = c[0],
                    ISOCode = c[1],
                    RegionId = regions.First(r => r.Name.Equals(c[3], StringComparison.OrdinalIgnoreCase)).Id
                });
            }

            dbContext.SaveChanges();
        }

        public static void AddDepartments(HRFlowDbContext dbContext)
        {
            var departmentNames = GetSeedData("Departments");

            foreach (var name in departmentNames)
            {
                dbContext.Departments.Add(new Department()
                {
                    Name = name,
                });
            }

            dbContext.SaveChanges();
        }

        public static void AddEmployees(HRFlowDbContext dbContext)
        {
            var employees = GetSeedData("Employees");

            foreach (var employee in employees.Skip(1))
            {
                var e = employee.Split(',');

                dbContext.Employees.Add(new Employee()
                {
                    FirstName = e[0],
                    MiddleName = e[1],
                    LastName = e[2],
                    IBAN = e[3],
                    HireDate = DateTime.Parse(e[4]),
                    LastModified = DateTime.Parse(e[5]),
                    LineManagerId = String.IsNullOrWhiteSpace(e[6]) ? null : int.Parse(e[6]),
                });
            }

            dbContext.SaveChanges();
        }

        private static void AddContacts(HRFlowDbContext dbContext)
        {
            var addresses = GetSeedData("Addresses");

            var employees = dbContext.Employees.ToList();

            var random = new Random();

            foreach (var employee in employees)
            {
                dbContext.Contacts.Add(new Contact()
                {
                    Address = addresses[random.Next(addresses.Length)],
                    Employee = employee,
                    PersonalEmail = employee.FirstName.ToLower() + "private.com",
                    CompanyEmail = employee.FirstName.ToLower() + "enterprise.com",
                    PhoneNumber = random.Next(50000000, 90000000).ToString(),
                    Gender = Gender.Male,
                });
            }

            dbContext.SaveChanges();
        }

        public static void AddJobs(HRFlowDbContext dbContext)
        {
            var jobs = GetSeedData("Jobs");

            foreach (var job in jobs)
            {
                var j = job.Split(',');

                dbContext.Jobs.Add(new Job()
                {
                    Title = j[0],
                    Description = j[1],
                });
            }

            dbContext.SaveChanges();
        }

        public static void AddJobHistories(HRFlowDbContext dbContext)
        {
            var jobs = dbContext.Jobs.ToList();

            var employees = dbContext.Employees.ToList();

            var random = new Random();

            foreach (var employee in employees)
            {
                var salary = random.Next(20, 120) * 100;
                var jobIndex = random.Next(jobs.Count);
                var paymentIncreases = random.Next(1, 3);


                var employeeJobHistories = new List<JobHistory>();

                for (int i = 0; i < paymentIncreases; i++)
                {
                    var lastJob = employeeJobHistories.LastOrDefault();

                    if (lastJob == null)
                    {
                        employeeJobHistories.Add(new JobHistory()
                        {
                            Employee = employee,
                            Job = jobs[jobIndex],
                            Salary = salary,
                            StartDate = employee.HireDate,
                            EndDate = paymentIncreases > 1 ? employee.HireDate.AddMonths(6) : null
                        });
                    }
                    else
                    {
                        employeeJobHistories.Add(new JobHistory()
                        {
                            Employee = employee,
                            Job = jobs[jobIndex],
                            Salary = salary * 1.20M,
                            StartDate = lastJob.EndDate!.Value,
                        });
                    }
                }

                dbContext.JobHistories.AddRange(employeeJobHistories);
            }

            dbContext.SaveChanges();
        }

        public static void AddDepartmentHistories(HRFlowDbContext dbContext)
        {
            var departments = dbContext.Departments.ToList();

            var employees = dbContext.Employees.ToList();

            var random = new Random();

            foreach (var employee in employees)
            {
                var departmentIndex = random.Next(departments.Count);

                dbContext.DepartmentHistories.Add(new DepartmentHistory()
                {
                    Employee = employee,
                    Department = departments[departmentIndex],
                    StartDate = employee.HireDate
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

            var entityNames = new string[] { "Employee", "Contact", "Department", "DepartmentHistory", "Job", "JobHistory" };

            foreach (var entityName in entityNames)
            {
                var entitiesToAdd = GetSeedData(entityName);

                dbContext.AddRange(entitiesToAdd);

                dbContext.SaveChanges();
            }
        }

        private static string[] GetSeedData(string fileName)
        {
            var directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var basePath = Path.GetFullPath(Path.Combine(directoryPath!, @"..\..\..\.."));

            string fullPath = Path.Combine(basePath, @$"HRFlow.Seed\{fileName}.txt");

            string[] lines = File.ReadAllLines(fullPath);

            return lines;
        }
    }
}
