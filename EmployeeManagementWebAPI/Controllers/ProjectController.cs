using AutoMapper;
using EmployeeManagement.Common.Model.RequestModel;
using EmployeeManagement.DAL.Model;
using EmployeeManagement.UnitOfWOrk.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ProjectController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet("get-project-by-id")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var project = await _uow.ProjectRepository.GetByIdAsync(id);
            if (project == null) return NotFound();

            return Ok(project);
        }
        [HttpGet("get-all-projects")]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _uow.ProjectRepository.GetAllAsync();
            return Ok(projects);
        }
        [HttpPost("add-new-project")]
        public async Task<IActionResult> AddNewProject(ProjectRequest req)
        {
            var project = _mapper.Map<Project>(req);
            project.CreatedTime = DateTime.Now;
            project.IsActive = true;
            project.IsDeleted = false;

            await _uow.ProjectRepository.AddAsync(project);
            await _uow.SaveChangeAsync();

            return Ok(project);
        }
        [HttpPut("update-project")]
        public async Task<IActionResult> UpdateProject(Guid id, Project value)
        {
            var project = await _uow.ProjectRepository.GetByIdAsync(id);
            if (project == null) return NotFound();

            project.Name = value.Name;
            project.Description = value.Description;
            project.ClientName = value.ClientName;
            project.StartDate = value.StartDate;
            project.ProposedEndDate = value.ProposedEndDate;
            project.ProjectState = value.ProjectState;

            _uow.ProjectRepository.Update(project);
            await _uow.SaveChangeAsync();

            return NoContent();
        }

        [HttpDelete("delete-project")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var project = await _uow.ProjectRepository.GetByIdAsync(id);
            if (project == null) return NotFound();

            _uow.ProjectRepository.Remove(project);
            await _uow.SaveChangeAsync();

            return NoContent();
        }
    }
}
