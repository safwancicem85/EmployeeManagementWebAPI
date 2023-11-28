using AutoMapper;
using EmployeeManagement.Common.Model.RequestModel;
using EmployeeManagement.DAL.Model;

namespace EmployeeManagementWebAPI.Helpers
{
    public class MapperProfile: Profile
    {
        public MapperProfile() {
            CreateMap<EmployeeRequest, Employee>().ReverseMap();
            CreateMap<ProjectRequest, Project>().ReverseMap();
            CreateMap<ProjectMemberRequest, ProjectMember>().ReverseMap();  
            CreateMap<EmployeeDayOffRequest, DayOffRequest>().ReverseMap();
        }
    }
}
