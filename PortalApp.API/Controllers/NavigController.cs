using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.Data;
using PortalApp.API.Models;

namespace PortalApp.API.Controllers
{
    // [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class NavigController : ControllerBase
    {
        
        private readonly DataContext _context;
        public NavigController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("getNavig")]
        public async Task<IActionResult> GetNavig()
        {
            var navList = new List<Navig>();

            var rootMenuItems = _context.Navigs.FromSql("SELECT * FROM Navigs where NavigId is null")
            .Select(x => x.Id).ToList<int>();
            // .ToList<Navig>();


            

            var values = await _context.Navigs.Include(x => x.Children)
            .Where(x => rootMenuItems.Contains(x.Id)).ToListAsync();

            

            return Ok(values);
        }
    }
}