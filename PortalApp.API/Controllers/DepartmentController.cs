using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalApp.API.Data;
using PortalApp.API.Data.Repo.DepartmentRepo;

namespace PortalApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IPortalRepository _repo;
        private readonly IDepartmentRepository _depRepo;

        public DepartmentController(IPortalRepository repo, IDepartmentRepository depRepo)
        {
            _repo = repo;
            _depRepo = depRepo;
        }

        
        [HttpGet("getDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            // if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //     return Unauthorized();

            var departments = await _depRepo.GetDepartments();

            if (departments == null)
                return NotFound();

            return Ok(departments);
        }

        [HttpGet("getDepartmentVs")]
        public async Task<IActionResult> GetDepartmentVs()
        {
            // if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //     return Unauthorized();

            var departmentVs = await _depRepo.GetDepartmentVs();

            if (departmentVs == null)
                return NotFound();

            return Ok(departmentVs);
        }
    }
}