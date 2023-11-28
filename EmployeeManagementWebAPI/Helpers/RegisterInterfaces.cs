using AutoMapper;
using EmployeeManagement.Service.IServices;
using EmployeeManagement.Service.Services;
using EmployeeManagement.UnitOfWOrk.Implementations;
using EmployeeManagement.UnitOfWOrk.Interfaces;

namespace EmployeeManagementWebAPI.Helpers
{
    public static class RegisterInterfaces
    {
        public static IServiceCollection AddMyDependencyGroup(
             this IServiceCollection services)
        {
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            }).CreateMapper());

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmployeeDayOffService, EmployeeDayOffService>();

            return services;
        }
    }
}
