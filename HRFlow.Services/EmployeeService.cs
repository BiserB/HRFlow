using HRFlow.Data;
using HRFlow.Services.Interfaces;

namespace HRFlow.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HRFlowDbContext dbContext;

        public EmployeeService(HRFlowDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddEmployee()
        {
            return dbContext.SaveChanges() > 0;            
        }
    }
}