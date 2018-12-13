using System.Collections.Generic;
using System.Threading.Tasks;
using PortalApp.API.Models;

namespace PortalApp.API.Data.Repo.RegionDepo
{
    public interface IRegionRepository
    {
         Task<IEnumerable<Region>> GetRegions();
    }
}