using static EmployeeManagement.Common.Helper.Enums;

namespace EmployeeManagement.DAL.Model
{
    public class Employee: ModelBase
    {
        public string FullName { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }
        public DateTime EmployeeDate { get; set; }
        public string EmailAddress { get; set; }
        public string HomeAddress { get; set; }
    }
}
