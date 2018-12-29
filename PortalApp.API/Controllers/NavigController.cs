using System;
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

        [HttpGet("getNavig/{lang}")]
        public async Task<IActionResult> GetNavig(string lang)
        {
            var allNavigs = _context.Navigs;

            if (lang != "ru")
            {
                foreach (var item in allNavigs)
                {
                    if (lang == "en")
                    {
                        item.Title = item.TitleEng;
                    }
                    else if (lang == "kz")
                    {
                        item.Title = item.TitleKaz;
                    }

                }
            }




            // var navList = new List<Navig>();

            var rootMenuItems = await _context.Navigs.FromSql("SELECT * FROM Navigs where NavigId is null")
            .Select(x => x.Id.Value).ToListAsync<Guid>();



            var values = await allNavigs
                 .Include(x => x.Children)
                .Where(x => rootMenuItems.Contains(x.Id.Value))
               .ToListAsync<Navig>();



            return Ok(values);

            // return Ok();
        }
    }
}