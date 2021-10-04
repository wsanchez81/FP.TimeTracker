using FP.TimeTracker.WebApi.Data;
using FP.TimeTracker.WebApi.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FP.TimeTracker.WebApi.Repository
{
    public class EmployeeTimeSheetRepository : IEmployeeTimeSheetRepository
    {
        private TimeTrackerDBContext _timeTrackerDB;
        public EmployeeTimeSheetRepository(TimeTrackerDBContext trackerDB)
        {
            _timeTrackerDB = trackerDB;
        }

        public async Task<int> ActivateEmployeeAsync(int employeeID)
        {
            var employee = await _timeTrackerDB.Employees.FirstOrDefaultAsync(item => item.ID == employeeID);
            if (employee != null)
            {
                employee.IsActive = true;
                _timeTrackerDB.SaveChanges();
            }
            return employeeID;
        }

        public async Task<Employee> AddUpdateEmployeeAsync(Employee employee)
        {
            var employeeDB = !string.IsNullOrEmpty(employee.EmployeeName)
              ? _timeTrackerDB.Employees.FirstOrDefault(item => item.EmployeeName == employee.EmployeeName)
               : _timeTrackerDB.Employees.FirstOrDefault(item => item.ID == employee.ID);
            if (employeeDB == null)
            {
                employeeDB = new Employee
                {
                    EmployeeName = employee.EmployeeName,
                    IsActive = true
                };
                 var result = await _timeTrackerDB.Employees.AddAsync(employeeDB);
                employee = result.Entity;
            }
            else
            {
                employeeDB.EmployeeName = employee.EmployeeName;
                employeeDB.IsActive = employee.IsActive;
            }
            _timeTrackerDB.SaveChanges();
            return employee;
        }

        public async Task<int> DeActivateEmployeeAsync(int employeeID)
        {
            var employee = await _timeTrackerDB.Employees.FirstOrDefaultAsync(item => item.ID == employeeID);
            if (employee != null)
            {
                employee.IsActive = false;
                _timeTrackerDB.SaveChanges();
            }
            return employeeID;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _timeTrackerDB.Employees.Include(item => item.TimeSheets).ToListAsync();
        }

        public async Task<List<EmployeeTimeSheet>> GetEmployeeTimeSheetsAsync()
        {
            return await _timeTrackerDB.EmployeeTimeSheets.Include(item => item.Employee).ToListAsync();

        }



        public async Task<EmployeeTimeSheet> AddUpdateEmployeeTimeSheetAsync(EmployeeTimeSheet timeSheet)
        {
            var employeeDB = !string.IsNullOrEmpty(timeSheet.Employee.EmployeeName)
               ? await  _timeTrackerDB.Employees.FirstOrDefaultAsync(item => item.EmployeeName == timeSheet.Employee.EmployeeName)
                : await  _timeTrackerDB.Employees.FirstOrDefaultAsync(item => item.ID == timeSheet.Employee.ID);
            if (employeeDB == null)
            {

                employeeDB = await AddUpdateEmployeeAsync(new Employee { EmployeeName = timeSheet.Employee.EmployeeName });
            }
            var timeSheetDb =  _timeTrackerDB.EmployeeTimeSheets.FirstOrDefaultAsync(item => item.Employee.ID == timeSheet.Employee.ID && item.DatePeriod.Date == timeSheet.DatePeriod.Date).Result;
            if (timeSheetDb == null)
            {
                timeSheetDb = new EmployeeTimeSheet();
                timeSheetDb.Employee = employeeDB;

            }
            timeSheetDb.ClockInTime = timeSheet.ClockInTime;
            timeSheetDb.ClockOutTime = timeSheet.ClockOutTime;
            timeSheetDb.DatePeriod = timeSheet.DatePeriod;
            if (timeSheetDb.ID <= 0)
            {
                await _timeTrackerDB.AddAsync(timeSheetDb);
            }
             await _timeTrackerDB.SaveChangesAsync();


            return  timeSheetDb;
        }

        public async Task<bool> DeleteEmployeeTimeSheetAsync(int timesheetID)
        {
            var timeSheetDb = await _timeTrackerDB.EmployeeTimeSheets.FirstOrDefaultAsync(item => item.ID == timesheetID);
            if (timeSheetDb != null)
            {
                _timeTrackerDB.EmployeeTimeSheets.Remove(timeSheetDb);
                _timeTrackerDB.SaveChanges();
            }
            return true;
        }

    }
}
