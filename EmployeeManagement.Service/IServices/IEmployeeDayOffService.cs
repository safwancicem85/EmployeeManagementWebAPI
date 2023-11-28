using EmployeeManagement.Common.Model.ResponseModel;

namespace EmployeeManagement.Service.IServices
{
    public interface IEmployeeDayOffService
    {
        public Task<bool> CheckIfEmployeeCanBeAssigned(Guid employeeId, int workPercent);
        public Task<EmployeeDayOffDetailResponse> GetEmployeeDayOffDetail(Guid employeeId);
    }
}
