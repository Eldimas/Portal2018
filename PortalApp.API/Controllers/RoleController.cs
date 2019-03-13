
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortalApp.API.Models;

namespace PortalApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly RoleManager<Role> _roleManager;
        public RoleController(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet("getAllRoles")]
        public IActionResult GetAllRoles()
        {
            var roles =  _roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpGet("getRoleById/{id}")]
        public IActionResult GetRoleById(int id)
        {
            var role =  _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            return Ok(role);
        }
    }
}