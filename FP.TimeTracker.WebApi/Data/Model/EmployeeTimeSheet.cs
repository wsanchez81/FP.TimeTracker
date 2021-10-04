using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FP.TimeTracker.WebApi.Data.Model
{
    public class EmployeeTimeSheet
    {
        public int ID { get; set; }
        public DateTime DatePeriod { get; set; }
        public DateTime ClockInTime { get; set; }
        public DateTime ClockOutTime { get; set; }

        public Employee Employee { get; set; }
    }
}
