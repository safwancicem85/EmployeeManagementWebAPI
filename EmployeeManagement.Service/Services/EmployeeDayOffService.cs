using EmployeeManagement.Common.Model.ResponseModel;
using EmployeeManagement.Service.IServices;
using EmployeeManagement.UnitOfWOrk.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Service.Services
{
    public class EmployeeDayOffService : IEmployeeDayOffService
    {
        private readonly IUnitOfWork _uow;
        public EmployeeDayOffService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<bool> CheckIfEmployeeCanBeAssigned(Guid employeeId, int workPercent)
        {
            var empProjects = await _uow.ProjectMemberRepository
                            .Querriable(x => x.EmployeeId == employeeId && x.IsActive && !x.IsDeleted)
                            .AsNoTracking()
                            .ToListAsync();

            return empProjects.Sum(x => x.WorkdLoadPercent) + workPercent <= 100;
        }

        public async Task<EmployeeDayOffDetailResponse> GetEmployeeDayOffDetail(Guid employeeId)
        {
            var empDayoffReq = await _uow.DayOffRequestRepository
                            .Querriable(x => x.EmployeeId == employeeId && x.IsApprove &&
                                        x.IsActive && !x.IsDeleted)
                            .AsNoTracking()
                            .ToListAsync();
            var employee = await _uow.EmployeeRepository.GetByIdAsync(employeeId);

            DateTime lastaniv = new(DateTime.Now.Year, employee.EmployeeDate.Month, employee.EmployeeDate.Day);
            DateTime nextaniv = lastaniv.AddYears(1);

            if (DateTime.Now.Date < lastaniv)
            {
                nextaniv = lastaniv;
                lastaniv = lastaniv.AddYears(-1);
            }

            int totalOffday = (lastaniv.Year - employee.EmployeeDate.Year) * 12;
            int usedDayoff = empDayoffReq?.Sum(x => x.DayCount) ?? 0;
            return new EmployeeDayOffDetailResponse()
            {
                TotalDayOff = totalOffday,
                UsedDayOff = usedDayoff,
                DayOffLeft = totalOffday - usedDayoff,
                DayOffIncrementDate = nextaniv.ToShortDateString(),
                EmployeeName = employee.FullName
            };
        }
    }
}
