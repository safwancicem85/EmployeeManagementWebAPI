using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmployeeManagement.Common.Helper.Enums;

namespace EmployeeManagement.Common.Model.RequestModel
{
    public class ProjectRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClientName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ProposedEndDate { get; set; }
        public ProjectState ProjectState { get; set; }
    }
}
