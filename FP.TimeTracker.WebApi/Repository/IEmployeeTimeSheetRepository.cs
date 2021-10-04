using FP.TimeTracker.WebApi.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FP.TimeTracker.WebApi.Repository
{
    public interface IEmployeeTimeSheetRepository
    {

        public  Task<List<EmployeeTimeSheet>> GetEmployeeTimeSheetsAsync();

        public Task<Employee> AddUpdateEmployeeAsync(Employee employee);

        public Task<EmployeeTimeSheet> AddUpdateEmployeeTimeSheetAsync(EmployeeTimeSheet timeSheet);


        public Task<bool> DeleteEmployeeTimeSheetAsync(int timesheetID);
        public Task<List<Employee>> GetEmployeesAsync();
        public  Task<int> ActivateEmployeeAsync(int employeeID);
        public Task<int> DeActivateEmployeeAsync(int employeeID);
    }
}
