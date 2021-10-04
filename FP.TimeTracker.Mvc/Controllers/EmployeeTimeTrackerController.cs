using FP.TimeTracker.Dto.Request;
using FP.TimeTracker.Dto.Respnse;
using FP.TimeTracker.Dto.Response;
using FP.TimeTracker.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FP.TimeTracker.Mvc.Controllers
{
    [Authorize]
    public class EmployeeTimeTrackerController : Controller
    {
        IHttpClientFactory _clientFactory;
        public EmployeeTimeTrackerController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var result = new EmployeesTimeSheetsViewModel() { EmployeeTimesSheets = new List<EmployeeTimeSheetModel> { } };

            var empsResponse = await GetFromHttpClient<List<EmployeeVM>>("TimeTracker/GetEmployeesAsync");
            var empTimeSheetsResponse = await GetFromHttpClient<List<EmployeeTimesheetVM>>("TimeTracker/GetEmployeeTimeSheetsAsync");
            if (empsResponse != null)
            {
                foreach (var employee in empsResponse)
                {
                    var employeeTimeSheets = empTimeSheetsResponse.Where(item => item.EmployeeID == employee.ID).OrderByDescending(item => item.DatePeriod).ToList();
                    var latestEmpPeriod = employeeTimeSheets.FirstOrDefault();
                    if (latestEmpPeriod == null || latestEmpPeriod.DatePeriod.Date != DateTime.Now.Date)
                    {
                        latestEmpPeriod = new EmployeeTimesheetVM
                        {
                            EmployeeID = employee.ID,
                            DatePeriod = DateTime.Now.Date,
                            TimeIn = DateTime.MinValue,
                            TimeOut = DateTime.MinValue
                        };
                    }
                    result.EmployeeTimesSheets.Add(new EmployeeTimeSheetModel
                    {
                        EmployeeID = employee.ID,
                        EmployeeName = employee.EmployeeName,
                        IsActive = employee.IsActive,
                        CurrentTimeSheet = new TimeSheetModel
                        {
                            TimeSheetID = latestEmpPeriod.TimeSheetId,
                            DatePeriod = latestEmpPeriod.DatePeriod,
                            TimeIn = latestEmpPeriod.TimeIn,
                            TimeOut = latestEmpPeriod.TimeOut
                        },
                        TimeSheets = employeeTimeSheets.Select(item => new TimeSheetModel
                        {
                            TimeSheetID = item.TimeSheetId,
                            DatePeriod = item.DatePeriod,
                            TimeIn = item.TimeIn,
                            TimeOut = item.TimeOut
                        }).ToList()
                    });
                }
            }
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTimeSheetJson(EmployeeTimeSheetRequest request)
        {

            var empTimeSheetsResponse = await PostToHttpClient<EmployeeTimeSheetRequest>("TimeTracker/SaveTimeSheetAsync", request);
            return Json(empTimeSheetsResponse);

        }
        [HttpPost]
        public async Task<IActionResult> SaveTimeSheet(EmployeeTimeSheetRequest request)
        {            
            var empTimeSheetsResponse = await PostToHttpClient<EmployeeTimeSheetRequest>("TimeTracker/SaveTimeSheetAsync", request);
            TempData.Add("Message", "Employee time sheet has been added");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Activate(int employeeID)
        {
            var data = new EmployeeVM { ID = employeeID };
            var empTimeSheetsResponse = await PostToHttpClient<EmployeeVM>("TimeTracker/ActivateEmployeeAsync", data);
            TempData.Add("Message", "Employee has been acitvated");    
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeActivate(int employeeID)
        {
            var data = new EmployeeVM { ID = employeeID };
            var empTimeSheetsResponse = await PostToHttpClient<EmployeeVM>("TimeTracker/DeActivateEmployeeAsync", data);
            TempData.Add("Message", "Employee has been deacitvated");
            return RedirectToAction("Index");
        }

        #region Helper Methods

        private async Task<HttpResponseMessage> PostToHttpClient<T>(string actionMethod, T data)
        {
            var client = getClient();
            if (client != null)
            {
                var response = await client.PostAsJsonAsync<T>(actionMethod, data);
                return response;
            }
            return null;
        }
        private async Task<T> GetFromHttpClient<T>(string actionMethod)
        {
            var client = getClient();
            if (client != null)
            {
                var response = await client.GetFromJsonAsync<T>(actionMethod);
                return response;
            }
            return default(T);
        }
        private HttpClient getClient()
        {
            var claimToken = User.Claims.FirstOrDefault(item => item.Type == "Token");
            if (claimToken != null)
            {
                var token = claimToken.Value.ToString();

                var client = _clientFactory.CreateClient("meta");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return client;
            }
            return null;
        }

        #endregion

    }
}
