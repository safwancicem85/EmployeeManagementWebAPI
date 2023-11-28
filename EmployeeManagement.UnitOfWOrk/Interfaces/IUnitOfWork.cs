using EmployeeManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.UnitOfWOrk.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        public IRepository<Project> ProjectRepository { get; }
        public IRepository<Employee> EmployeeRepository { get; }
        public IRepository<DayOffRequest> DayOffRequestRepository { get; }
        public IRepository<ProjectMember> ProjectMemberRepository { get; }
        public Task<int> SaveChangeAsync();
        public int SaveChange();
    }
}
