using HRFlow.Common.BindingModels;
using HRFlow.Common.ViewModels;
using HRFlow.Data;
using HRFlow.Entities;
using HRFlow.Entities.Enums;
using HRFlow.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HRFlow.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HRFlowDbContext dbContext;

        public EmployeeService(HRFlowDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddComment(AddCommentModel model)
        {
            var comment = new Comment()
            {
                EmployeeId = model.EmployeeId,
                AuthorId = 1,
                Content = model.Comment,
                LastModified = DateTime.Now,
            };

            dbContext.Comments.Add(comment);

            return dbContext.SaveChanges() > 0;
        }

        public IList<EmployeeViewModel> GetAllEmployees(bool onlyActiveEmployees)
        {
            var today = DateTime.Today;

            IList<EmployeeViewModel> employeeModels;

            IQueryable<Employee> employees = dbContext.Employees;

            if (onlyActiveEmployees)
            {
                employees = employees.Where(e => e.TerminationDate == null);
            }

            employeeModels = employees
                    .Select(e => new EmployeeViewModel()
                    {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        MiddleName = e.MiddleName,
                        LastName = e.LastName,
                        LineManagerName = e.LineManager != null ? e.LineManager.LastName : String.Empty,
                        HireDate = e.HireDate.ToShortDateString(),
                        LongTermEmployee = (today.Date - e.HireDate.Date).Days.ToString()
                    })
                    .OrderBy(e => e.Id)
                    .ToList();

            return employeeModels;
        }

        public EmployeeDetailsViewModel GetEmployee(int id)
        {
            var employee = dbContext.Employees
                .Include(e => e.LineManager)
                .Include(e => e.Contact)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return null;
            }

            var departmentHistory = dbContext.DepartmentHistories
                .Include(dh => dh.Department)
                .Where(dh => dh.EmployeeId == employee.Id)
                .OrderByDescending(dh => dh.StartDate)
                .Select(dh => new DepartmentHistoryViewModel()
                {
                    Id = dh.Id,
                    DepartmentId = dh.DepartmentId,
                    DepartmentName = dh.Department.Name,
                    StartDate = dh.StartDate,
                    EndDate = dh.EndDate,
                })
                .ToList();

            var jobHistory = dbContext.JobHistories
                .Include(dh => dh.Job)
                .Where(jh => jh.EmployeeId == employee.Id)
                .OrderByDescending(dh => dh.StartDate)
                .Select(jh => new JobHistoryViewModel()
                {
                    Id = jh.Id,
                    JobId = jh.JobId,
                    JobTitle = jh.Job.Title,
                    Salary = jh.Salary,
                    StartDate = jh.StartDate,
                    EndDate = jh.EndDate,
                })
                .ToList();

            var comments = dbContext.Comments
                .Include(c => c.Author)
                .Where(c => c.EmployeeId == employee.Id && !c.IsDeleted)
                .OrderByDescending(c => c.LastModified)
                .Select(c => new CommentViewModel()
                {
                    Id = c.Id,
                    EmployeeId = c.EmployeeId,
                    AuthorId = c.Author.Id,
                    AuthorName = c.Author.FirstName + " " + c.Author.LastName,
                    Content = c.Content,
                    LastModified = c.LastModified.ToShortDateString(),
                })
                .ToList();

            var lastJob = GetLastJob(employee);

            var model = new EmployeeDetailsViewModel()
            {
                Id = id,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                HireDate = employee.HireDate.ToShortDateString(),
                IBAN = employee.IBAN,
                Departments = departmentHistory,
                Jobs = jobHistory,
                Comments = comments,
                Salary = lastJob.Salary,
            };

            model.LineManagers.AddRange(GetManagerSelectList(employee.LineManager));

            model.Address = employee.Contact.Address;

            return model;
        }

        public bool UpdateEmployee(UpdateEmployeeModel model)
        {
            try
            {
                var employee = dbContext.Employees.Include(e => e.Contact).FirstOrDefault(e => e.Id == model.Id);

                if (employee == null)
                {
                    return false;
                }

                var now = DateTime.Now;

                employee.FirstName = model.FirstName;
                employee.MiddleName = model.MiddleName;
                employee.LastName = model.LastName;
                employee.IBAN = model.IBAN;
                employee.LastModified = now;
                employee.Contact.Address = model.Address;

                var lineManagerExists = dbContext.Employees.Any(e => e.Id == model.LineManagerId);

                employee.LineManagerId = lineManagerExists ? model.LineManagerId : null;                               

                var lastJob = GetLastJob(employee);

                if (model.Salary != lastJob.Salary)
                {
                    lastJob.EndDate = now;

                    employee.JobHistories.Add(new JobHistory()
                    {
                        EmployeeId = employee.Id,
                        JobId = lastJob.JobId,
                        Salary = model.Salary,
                        StartDate = now,
                    });
                }

                return dbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public AddEmployeeViewModel GetEmployeeModel()
        {
            var managers = GetManagerSelectList();

            var departments = dbContext.Departments
                .Select(d => new SelectListItem() { Value = d.Id.ToString(), Text = d.Name })
                .ToList();

            var jobs = dbContext.Jobs
                .Select(d => new SelectListItem() { Value = d.Id.ToString(), Text = d.Title })
                .ToList();

            var model = new AddEmployeeViewModel() { HireDate = DateTime.Now };

            
            model.Departments.Add(new SelectListItem() { Value = "0", Text = " - - - " });
            model.Jobs.Add(new SelectListItem() { Value = "0", Text = " - - - " });

            model.LineManagers.AddRange(managers);
            model.Departments.AddRange(departments);
            model.Jobs.AddRange(jobs);

            return model;
        }

        public int AddEmployee(AddEmployeeModel model)
        {
            var now = DateTime.Now;

            var employee = new Employee()
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                IBAN = model.IBAN,
                HireDate = model.HireDate,
                LastModified = now,
                LineManagerId = model.LineManagerId
            };

            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();

            var departmentHistory = new DepartmentHistory()
            {
                EmployeeId = employee.Id,
                DepartmentId = model.DepartmentId,
                StartDate = model.HireDate,
                LastModified = now,
            };

            var jobHistory = new JobHistory()
            {
                EmployeeId = employee.Id,
                JobId = model.JobId,
                StartDate = model.HireDate,
                Salary = model.Salary,
            };

            var random = new Random();

            var contact = new Contact()
            {
                Address = model.Address,
                Employee = employee,
                PersonalEmail = employee.FirstName.ToLower() + "private.com",
                CompanyEmail = employee.FirstName.ToLower() + "enterprise.com",
                PhoneNumber = random.Next(50000000, 90000000).ToString(),
                Gender = Gender.Male,
            };

            dbContext.Contacts.Add(contact);
            dbContext.DepartmentHistories.Add(departmentHistory);
            dbContext.JobHistories.Add(jobHistory);
            dbContext.SaveChanges();

            return employee.Id;
        }

        private string FormatName(string firstName, string middleName, string lastName)
        {
            var middleNameLetter = middleName != null ? middleName.Substring(0, 1) : String.Empty;

            return $"{ firstName ?? "" } { middleNameLetter } { lastName ?? "" }";
        }

        private JobHistory GetLastJob(Employee employee)
        {
            var lastJob = dbContext.JobHistories
                .OrderByDescending(jh => jh.StartDate)
                .FirstOrDefault(jh => jh.EmployeeId == employee.Id);

            if (lastJob == null)
            {
                throw new ApplicationException($"No job found for an employee Id:{employee.Id} {employee.FirstName}");
            }

            return lastJob;
        }

        public bool UpdateComment(UpdateCommentModel model)
        {
            var comment = dbContext.Comments.FirstOrDefault(c => c.Id == model.Id && c.EmployeeId == c.EmployeeId);

            if (comment == null)
            {
                return false;
            }

            comment.Content = model.Content;
            comment.LastModified = DateTime.Now;

            return dbContext.SaveChanges() > 0;
        }

        private List<SelectListItem> GetManagerSelectList(Employee lineManager = null)
        {
            var managerId = lineManager != null ? lineManager.Id : -1;

            var lineManagers = new List<SelectListItem>();
            
            lineManagers.Add(new SelectListItem() { Value = "0", Text = " - - - " });

            lineManagers.AddRange(dbContext.Employees
                .Where(e => e.Subordinates.Count > 0)
                .Select(e => new SelectListItem() { Value = e.Id.ToString(), Text = e.FirstName + " " + e.LastName, Selected = e.Id == managerId })
                .ToList());

            return lineManagers;
        }
    }
}