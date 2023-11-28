using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Common.Model.RequestModel
{
    public class EmployeeDayOffRequest
    {
        public int DayCount { get; set; }
        public DateTime DayOffRequestTime { get; set; }
        public DateTime RequestTime { get; set; }
        public string RequestReason { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
