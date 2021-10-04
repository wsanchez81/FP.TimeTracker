using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FP.TimeTracker.Mvc.Models
{
    public class EmployeeTimeSheetModel
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public bool IsActive { get; set; }
        public TimeSheetModel CurrentTimeSheet { get; set; }
        public List<TimeSheetModel> TimeSheets { get; set; }


    }
}
