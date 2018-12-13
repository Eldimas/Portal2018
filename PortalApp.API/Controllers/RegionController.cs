using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalApp.API.Data;
using PortalApp.API.Data.Repo.RegionDepo;

namespace PortalApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController: ControllerBase
    {
        private readonly IPortalRepository _repo;
        private readonly IRegionRepository _regionRepo;

        public RegionController(IPortalRepository repo, IRegionRepository regionRepo)
        {
            _repo = repo;
            _regionRepo = regionRepo;
        }
        

        [HttpGet("getRegions")]
        public async Task<IActionResult> GetRegions() {
            
            var regions = await _regionRepo.GetRegions();

            if(regions == null){
                return NotFound();
            }
            return Ok(regions);
        }

    }
}