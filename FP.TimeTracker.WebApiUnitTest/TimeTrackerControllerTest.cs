using FP.TimeTracker.WebApi.Controllers;
using NUnit.Framework;
using Moq;
using FP.TimeTracker.WebApi.Repository;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using FP.TimeTracker.WebApi.Data.Model;
using System.Linq;

namespace FP.TimeTracker.WebApiUnitTest
{
    public class TimeTrackerControllerTest

    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task  GetEmployeesAsync_ShouldCall_Repositories_GetEmployees()
        {
            var employees = new List<Employee>
                {
                    new Employee
                    {
                         ID = 1,
                          EmployeeName  = "Moq Emploee Name"
                    },
                     new Employee
                    {
                         ID = 2,
                          EmployeeName  = "Moq Emploee Name 2"
                    }
                };
            //Mocks
            var mockEmployeeRepository = new Mock<IEmployeeTimeSheetRepository>();
            mockEmployeeRepository.Setup(item => item.GetEmployeesAsync())
                .Returns(Task.FromResult(employees));
            // Invoke
            var controller = new TimeTrackerController(mockEmployeeRepository.Object);
            var result =  await controller.GetEmployeesAsync();
            
            //Aassert
            mockEmployeeRepository.Verify(item => item.GetEmployeesAsync(), Times.Once);
            Assert.AreEqual(employees.Count, result.Count());
            Assert.AreEqual(employees[1].EmployeeName, result.ElementAt(1).EmployeeName);
        }
    }
}