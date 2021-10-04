using FP.TimeTracker.Dto.Request;
using FP.TimeTracker.Dto.Respnse;
using FP.TimeTracker.Dto.Response;
using FP.TimeTracker.WebApi.Data.Model;
using FP.TimeTracker.WebApi.Flters;
using FP.TimeTracker.WebApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FP.TimeTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenAuthenticationFilter]
    public class TimeTrackerController : ControllerBase
    {
        private readonly IEmployeeTimeSheetRepository _employeeRepository;
        public TimeTrackerController(IEmployeeTimeSheetRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        [HttpGet]
        [Route("GetEmployeesAsync")]
        public Task<IEnumerable<EmployeeVM>> GetEmployeesAsync()
        {
            var timeSheets = _employeeRepository.GetEmployeesAsync().Result;
            var result =  timeSheets.Select(item => new EmployeeVM
            {
                ID = item.ID,
                EmployeeName = item.EmployeeName,
                IsActive = item.IsActive
            });

            return Task.FromResult( result);

        }

        [HttpGet]
        [Route("GetEmployeeTimeSheetsAsync")]
        public Task<IEnumerable<EmployeeTimesheetVM>> GetEmployeeTimeSheetsAsync()
        {
            var timeSheets = _employeeRepository.GetEmployeeTimeSheetsAsync().Result;
            var result = timeSheets.Select(item => new EmployeeTimesheetVM
            {
                EmployeeID = item.Employee.ID,
                TimeIn = item.ClockInTime,
                TimeOut = item.ClockOutTime,
                DatePeriod = item.DatePeriod,
                TimeSheetId = item.ID
            });

            return Task.FromResult(result);

        }
        [HttpPost]
        [Route("SaveTimeSheetAsync")]
        public async Task<EmployeeTimeSheetRequest> SaveTimeSheetAsync(EmployeeTimeSheetRequest request)
        {
            var employeeTimeSheetRec = new EmployeeTimeSheet
            {
                Employee = new Employee { ID = request.EmployeeID, EmployeeName = request.EmployeeName },
                DatePeriod = request.DatePeriod == null? DateTime.MinValue :  DateTime.ParseExact(request.DatePeriod, "MMM-dd-yyyy", null),
                ClockInTime = request.TimeIn == null? DateTime.MinValue: DateTime.ParseExact(request.TimeIn, "hh:mm tt", null),
                ClockOutTime = request.TimeOut == null ? DateTime.MinValue : DateTime.ParseExact(request.TimeOut, "hh:mm tt", null),
            };
            var timeSheets = await _employeeRepository.AddUpdateEmployeeTimeSheetAsync(employeeTimeSheetRec);

            return request;

        }
        [HttpPost]
        [Route("ActivateEmployeeAsync")]
        public async Task<int> ActivateEmployeeAsync(EmployeeVM employee)
        {
            
            int eployeeID = await _employeeRepository.ActivateEmployeeAsync(employee.ID);

            return  eployeeID;

        }
        [HttpPost]
        [Route("DeActivateEmployeeAsync")]
        public async Task<int> DeActivateEmployeeAsync(EmployeeVM employee)
        {

            int eployeeID = await _employeeRepository.DeActivateEmployeeAsync(employee.ID);

            return eployeeID;

        }

    }
}
