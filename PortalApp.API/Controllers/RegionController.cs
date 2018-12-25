using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalApp.API.Data;
using PortalApp.API.Data.Repo.RegionDepo;
using PortalApp.API.Dtos;
using PortalApp.API.Models;

namespace PortalApp.API.Controllers
{
    // [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IPortalRepository _repo;
        private readonly IRegionRepository _regionRepo;

        private readonly IMapper _mapper;

        public RegionController(IPortalRepository repo, IRegionRepository regionRepo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
            _regionRepo = regionRepo;
        }


        [HttpGet("getRegions")]
        public async Task<IActionResult> GetRegions()
        {

            var regions = await _regionRepo.GetRegions();

            if (regions == null)
            {
                return NotFound();
            }
            return Ok(regions);
        }

        [HttpGet("{id}", Name = "GetRegion")]
        public async Task<IActionResult> GetRegion(Guid id)
        {
            // var isCurrentUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) == id;
            // var isCurrentUser = false;

            var region = await _regionRepo.GetRegion(id);

            // var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            // return Ok(userToReturn);
            return Ok(region);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegion(Guid id, RegionForUpdateDto regionForUpdateDto)
        {
            var regionFromRepo = await _regionRepo.GetRegion(id);

            _mapper.Map(regionForUpdateDto, regionFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("addRegion")]
        public async Task<IActionResult> AddRegion(RegionForUpdateDto regionForUpdateDto)
        {
            var regionToCreate = _mapper.Map<Region>(regionForUpdateDto);
            _repo.Add(regionToCreate);

            if (await _repo.SaveAll())
            {
                return Ok(regionToCreate);
            }
                


           return BadRequest();
            //return Ok();
        }

       
        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveRegion(Guid id)
        {
            var regionToRemove = await _regionRepo.GetRegion(id);
            _repo.Delete(regionToRemove);

            if (await _repo.SaveAll())
            {
                return Ok();
            }
                


           return BadRequest();
            //return Ok();
        }


    }
}