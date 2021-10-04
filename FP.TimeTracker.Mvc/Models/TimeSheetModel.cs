using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FP.TimeTracker.Mvc.Models
{
    public class TimeSheetModel
    {
        public int TimeSheetID { get; set; }
        public DateTime DatePeriod { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
    }
}
