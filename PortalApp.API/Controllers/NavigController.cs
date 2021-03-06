using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.Data;
using PortalApp.API.Dtos;
using PortalApp.API.Models;

namespace PortalApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class NavigController : ControllerBase
    {
        private readonly IPortalRepository _repo;
        private readonly DataContext _context;
        public NavigController(IPortalRepository repo, DataContext context)
        {
            _repo = repo;
            _context = context;
        }

        [HttpGet("getNavigById/{id}")]
        public async Task<IActionResult> GetNavigById(string id)
        {
            var navig = await _context.Navigs.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            return Ok(navig);
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

        [HttpPost("addId")]
        public async Task<IActionResult> AddId(int id)
        {
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(NavigUpdateDto navigUpdateDto)
        {
            var navig = await _context.Navigs.FirstOrDefaultAsync(x => x.Id == navigUpdateDto.Id);
            if (navig != null)
            {
                navig.Title = navigUpdateDto.Title;
                navig.TitleEng = navigUpdateDto.TitleEng;
                navig.TitleKaz = navigUpdateDto.TitleKaz;
                navig.Icon = navigUpdateDto.Icon;
                navig.Type = navigUpdateDto.Type;
                navig.Url = navigUpdateDto.Url;
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNavig(Guid id)
        {
            var navig = _context.Navigs.FirstOrDefault(x => x.Id == id);

            // _context.Navigs.Remove(navig);
            // _context.SaveChanges();
            _repo.Delete(navig);
            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete the navig");


        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(NavigUpdateDto navigUpdateDto)
        {
            var parentNavig = new Navig();

            if (navigUpdateDto.ParentId != Guid.Empty)
            {
                parentNavig = await _context.Navigs.FirstOrDefaultAsync(x => x.Id == navigUpdateDto.ParentId);
                if (parentNavig == null)
                {
                    return BadRequest("error");
                }

                if (parentNavig.Children == null)
                {
                    parentNavig.Children = new List<Navig>();
                    parentNavig.Type = "group";
                }

                var newNav = new Navig()
                {
                    Id = navigUpdateDto.Id,
                    Title = navigUpdateDto.Title,
                    TitleEng = navigUpdateDto.TitleEng,
                    TitleKaz = navigUpdateDto.TitleKaz,
                    Icon = navigUpdateDto.Icon,
                    Type = "item",
                    Url = navigUpdateDto.Url
                };

                parentNavig.Children.Add(newNav);
            }
            else
            {
                 var newNav = new Navig()
                {
                    Id = navigUpdateDto.Id,
                    Title = navigUpdateDto.Title,
                    TitleEng = navigUpdateDto.TitleEng,
                    TitleKaz = navigUpdateDto.TitleKaz,
                    Icon = navigUpdateDto.Icon,
                    Type = "item",
                    Url = navigUpdateDto.Url
                };
                _context.Navigs.Add(newNav);
            }




            _context.SaveChanges();


            return Ok();
        }
    }
}