using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FP.TimeTracker.WebApi.Data.Model
{
    public class Employee
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public bool IsActive { get; set; }

        public ICollection<EmployeeTimeSheet> TimeSheets { get; set; }
    }
}
