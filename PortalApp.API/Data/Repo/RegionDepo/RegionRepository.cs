using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Region> GetRegion(Guid id)
        {
            var region = await _context.Regions
            .Include(r=> r.DepartmentVs)
            .ThenInclude(dep => dep.Department)
            .FirstOrDefaultAsync(d=> d.Id == id);
            ///////////////////

            // foreach (var depV in region.DepartmentVs)
            // {
            //     depV.Department.DepartmentVs = null;
            // }
            /////////////
            var departmentVs = new List<DepartmentV>();
            foreach (var depV in region.DepartmentVs.OrderBy(r => r.Created))
            {
                if(departmentVs.FirstOrDefault(r =>r.DepartmentId == depV.DepartmentId) == null)
                {
                    depV.Department.DepartmentVs = null;
                    departmentVs.Add(depV);
                }
            }

            region.DepartmentVs = departmentVs;
           ////////////////////////////

            return region;
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