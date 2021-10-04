using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FP.TimeTracker.Dto.Response
{
    public class EmployeeTimesheetVM
    {
        public int EmployeeID { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }

        public DateTime DatePeriod { get; set; }

        public int TimeSheetId { get; set; }
    }
}
