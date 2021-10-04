using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.TimeTracker.Dto.Request
{
    public class EmployeeTimeSheetRequest
    {

        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string DatePeriod { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
    }
}
