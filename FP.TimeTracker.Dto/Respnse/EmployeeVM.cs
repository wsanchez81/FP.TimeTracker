using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.TimeTracker.Dto.Respnse
{
    public class EmployeeVM
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public bool IsActive { get; set; }
    }
}
