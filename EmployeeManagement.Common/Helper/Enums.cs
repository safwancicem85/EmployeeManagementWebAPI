using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Common.Helper
{
    public static class Enums
    {
        public enum EmployeeStatus
        {
            Management,
            Admin,
            HR,
            Employee
        }
        public enum ProjectState
        {
            Design,
            Development,
            Production,
            Delivered,
            Maintainance
        }
        public enum ProjectRole
        {
            Manager,
            BA,
            QA,
            Leader,
            Developer
        }
    }
}
