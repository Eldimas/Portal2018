using System.Collections.Generic;
using System.Threading.Tasks;
using PortalApp.API.Models;

namespace PortalApp.API.Data.Repo.DepartmentRepo
{
    public interface IDepartmentRepository
    {
         Task<IEnumerable<Department>> GetDepartments();
         Task<IEnumerable<DepartmentV>> GetDepartmentVs();
    }
}