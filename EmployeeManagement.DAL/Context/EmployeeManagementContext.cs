using EmployeeManagement.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DAL.Context
{
    public class EmployeeManagementContext: DbContext
    {
        public EmployeeManagementContext(DbContextOptions<EmployeeManagementContext> options) : base(options) { }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<DayOffRequest> DayOffRequest { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectMember> ProjectMember { get; set; }
    }
}
