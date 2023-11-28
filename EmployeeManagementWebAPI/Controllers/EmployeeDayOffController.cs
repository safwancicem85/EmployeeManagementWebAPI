using EmployeeManagement.UnitOfWOrk.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDayOffController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public EmployeeDayOffController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("get-all-request")]
        public async Task<IActionResult> GetAllAsync()
        {
            var reqs = await _uow.DayOffRequestRepository.GetAllAsync();
            return Ok(reqs);
        }
    }
}
