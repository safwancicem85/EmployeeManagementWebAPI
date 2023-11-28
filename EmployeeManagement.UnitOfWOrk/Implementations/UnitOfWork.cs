using EmployeeManagement.DAL.Context;
using EmployeeManagement.DAL.Model;
using EmployeeManagement.UnitOfWOrk.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.UnitOfWOrk.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeeManagementContext _context;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<ProjectMember> _ProjectMemberRepository;
        private readonly IRepository<DayOffRequest> _dayOffRequestRepository;
        public UnitOfWork(EmployeeManagementContext context)
        {
            _context = context;
            _ProjectMemberRepository = new Repository<ProjectMember>(context);
            _projectRepository = new Repository<Project>(context);
            _employeeRepository = new Repository<Employee>(context);
            _dayOffRequestRepository = new Repository<DayOffRequest>(context);
        }

        public IRepository<Project> ProjectRepository
        {
            get
            {
                if (_projectRepository == null)
                    return new Repository<Project>(_context);
                return _projectRepository;
            }
        }

        public IRepository<Employee> EmployeeRepository {
            get
            {
                if (_employeeRepository == null)
                    return new Repository<Employee>(_context);
                return _employeeRepository;
            }
        }

        public IRepository<DayOffRequest> DayOffRequestRepository {
            get
            {
                if (_dayOffRequestRepository == null)
                    return new Repository<DayOffRequest>(_context);
                return _dayOffRequestRepository;
            }
        }

        public IRepository<ProjectMember> ProjectMemberRepository {
            get
            {
                if (_ProjectMemberRepository == null)
                    return new Repository<ProjectMember>(_context);
                return _ProjectMemberRepository;
            }
        }

        public void Dispose() => _context.Dispose();

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int SaveChange()
        {
            return _context.SaveChanges();
        }
    }
}
