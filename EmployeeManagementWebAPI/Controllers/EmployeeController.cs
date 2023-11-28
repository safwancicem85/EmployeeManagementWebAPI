using AutoMapper;
using EmployeeManagement.Common.Model.RequestModel;
using EmployeeManagement.DAL.Model;
using EmployeeManagement.Service.IServices;
using EmployeeManagement.UnitOfWOrk.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace EmployeeManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IEmployeeDayOffService _employeeDayOffService;
        public EmployeeController(IUnitOfWork uow, 
            IMapper mapper,
            IEmployeeDayOffService employeeDayOffService)
        {
            _uow = uow;
            _mapper = mapper;
            _employeeDayOffService = employeeDayOffService;
        }

        [HttpGet("get-employee-by-id")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _uow.EmployeeRepository.GetByIdAsync(id);
            if(employee == null) return NotFound();

            return Ok(employee);
        }
        [HttpGet("get-all-employees")]
        public IActionResult GetAllEmployee()
        {
            var employees = _uow.EmployeeRepository.GetAll();
            return Ok(employees);
        }
        [HttpPost("add-new-employee")]
        public async Task<IActionResult> AddNewEmployee(EmployeeRequest employee)
        {
            var employeeToAdd = _mapper.Map<Employee>(employee);
            employeeToAdd.CreatedTime = DateTime.Now;
            employeeToAdd.IsActive = true;
            employeeToAdd.IsDeleted = false;

            await _uow.EmployeeRepository.AddAsync(employeeToAdd);
            await _uow.SaveChangeAsync();

            return Ok(employeeToAdd);
        }
        [HttpPut("update-employee")]
        public async Task<IActionResult> UpdateEmployee(Guid id, Employee value)
        {
            var employee = await _uow.EmployeeRepository.GetByIdAsync(id);
            if(employee == null) return NotFound();

            employee.FullName = value.FullName;
            employee.EmployeeDate = value.EmployeeDate;
            employee.EmailAddress = value.EmailAddress;
            employee.HomeAddress = value.HomeAddress;
            employee.EmployeeStatus = value.EmployeeStatus;

            _uow.EmployeeRepository.Update(employee);
            await _uow.SaveChangeAsync();

            return NoContent();
        }

        [HttpDelete("delete-employee")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _uow.EmployeeRepository.GetByIdAsync(id);
            if (employee == null) return NotFound();

            _uow.EmployeeRepository.Remove(employee);
            await _uow.SaveChangeAsync();

            return NoContent();
        }
        [HttpGet("get-employee-dayoff-detail")]
        public async Task<IActionResult> GetEmployeeDayOffDetail(Guid employeeId)
        {
            var detail = await _employeeDayOffService.GetEmployeeDayOffDetail(employeeId);
            return Ok(detail);
        }
        [HttpPost("assign-employee-to-project")]
        public async Task<IActionResult> AssignEmployeeToProject(ProjectMemberRequest req)
        {
            bool canBeAssigned = await _employeeDayOffService.CheckIfEmployeeCanBeAssigned(req.EmployeeId, req.WorkdLoadPercent);
            if (!canBeAssigned) return BadRequest("Employee will be overwork. Please reduce workload on another project!");

            var employee = await _uow.EmployeeRepository.GetByIdAsync(req.EmployeeId);
            var project = await _uow.ProjectRepository.GetByIdAsync(req.ProjectId);

            var assignProject = _mapper.Map<ProjectMember>(req);
            assignProject.CreatedTime = DateTime.Now;
            assignProject.IsActive = true;
            assignProject.IsDeleted = false;

            await _uow.ProjectMemberRepository.AddAsync(assignProject);
            await _uow.SaveChangeAsync();

            return Ok($"{employee.FullName} successfully assigned to project {project.Name}");
        }
        [HttpPost("employee-request-for-off")]
        public async Task<IActionResult> RequestForTimeOff(EmployeeDayOffRequest req)
        {
            var dayoffreq = _mapper.Map<DayOffRequest>(req);
            dayoffreq.IsApprove = false;
            dayoffreq.CreatedTime = DateTime.Now;
            dayoffreq.IsActive = true;
            dayoffreq.IsDeleted = false;

            await _uow.DayOffRequestRepository.AddAsync(dayoffreq);
            await _uow.SaveChangeAsync();
            return Ok(dayoffreq);
        }
        [HttpGet("response-employee-day-off-request")]
        public async Task<IActionResult> ResponseEmployeeDayoffReq(Guid id, bool responseStatus)
        {
            var empreq = await _uow.DayOffRequestRepository.GetByIdAsync(id);
            if (empreq == null) return NotFound();

            empreq.IsApprove = responseStatus;
            empreq.ApprovedTime = DateTime.Now;

            _uow.DayOffRequestRepository.Update(empreq);
            await _uow.SaveChangeAsync();

            return Ok("Employee request had been updated!");
        }
    }
}
