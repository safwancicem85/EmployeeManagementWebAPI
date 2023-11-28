using static EmployeeManagement.Common.Helper.Enums;

namespace EmployeeManagement.Common.Model.RequestModel
{
    public class EmployeeRequest
    {
        public string FullName { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }
        public DateTime EmployeeDate { get; set; }
        public string EmailAddress { get; set; }
        public string HomeAddress { get; set; }
    }
}
