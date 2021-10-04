using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FP.TimeTracker.WebApi.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace FP.TimeTracker.WebApi.Data
{
    public class TimeTrackerDBContext : DbContext
    {
        public TimeTrackerDBContext(DbContextOptions<TimeTrackerDBContext> options): base(options)
        {

        }
        // Entities        
        public DbSet<EmployeeTimeSheet> EmployeeTimeSheets { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
