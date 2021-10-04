using FP.TimeTracker.WebApi.Data;
using FP.TimeTracker.WebApi.Data.Model;
using FP.TimeTracker.WebApi.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FP.TimeTracker.WebApiUnitTest
{
    public class EmployeeTimeSheetRepositoryTest
    {
        [Test]
        public async Task GetEmployeesAsync_ShouldCall_DBContext_GetEmployees()
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

            var options = new DbContextOptionsBuilder<TimeTrackerDBContext>().UseInMemoryDatabase(databaseName: "EmployeeTimeTracker")
            .Options;
            // Insert seed data into the database using one instance of the context
            using (var context = new TimeTrackerDBContext(options))
            {
                context.Employees.AddRange(employees);
                context.SaveChanges();



                // Invoke
                var repository = new EmployeeTimeSheetRepository(context);
                var result = await repository.GetEmployeesAsync();

                //Aassert
                Assert.AreEqual(employees.Count, result.Count());
                Assert.AreEqual(employees[1].EmployeeName, result.ElementAt(1).EmployeeName);
            }
        }
    }
}
