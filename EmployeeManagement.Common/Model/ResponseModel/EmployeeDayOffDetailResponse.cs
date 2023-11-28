using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Common.Model.ResponseModel
{
    public class EmployeeDayOffDetailResponse
    {
        public string EmployeeName { get; set; }
        public int TotalDayOff { get; set; }
        public int UsedDayOff { get; set; }
        public int DayOffLeft { get; set; }
        public string DayOffIncrementDate { get; set; }
    }
}
