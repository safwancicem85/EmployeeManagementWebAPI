using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmployeeManagement.Common.Helper.Enums;

namespace EmployeeManagement.Common.Model.RequestModel
{
    public class ProjectMemberRequest
    {
        public Guid EmployeeId { get; set; }
        public Guid ProjectId { get; set; }
        public ProjectRole Role { get; set; }
        public int WorkdLoadPercent { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
