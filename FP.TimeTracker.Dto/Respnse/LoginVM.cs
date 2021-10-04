using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FP.TimeTracker.Dto.Response
{
    public class LoginVM
    {
        public string Token { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
