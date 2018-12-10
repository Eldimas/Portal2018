using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.Models;

namespace PortalApp.API.Data.Repo.RegionDepo
{
    public class RegionRepository : IRegionRepository
    {
        private readonly DataContext _context;

        public RegionRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Region>> GetRegions()
        {
           var regions = await _context.Regions
            .Include(r => r.DepartmentVs)
        //    .ThenInclude(x=>x.Users)
           .ToListAsync();
           return regions;
        }
    }
}