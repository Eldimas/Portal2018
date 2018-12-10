using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.Models;

namespace PortalApp.API.Data.Repo.DepartmentRepo
{
    public class DepartmentRepository : IDepartmentRepository
    {

        private readonly DataContext _context;
        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Department>> GetDepartments()
        {


            var departments = await _context.Departments
             .Include(x => x.DepartmentVs)

             //    .Include(x=>x.Users)
             // .Include(u => u.Sender).ThenInclude(p => p.Photos)
             // .Include(u => u.Recipient).ThenInclude(p => p.Photos)
             // .Where(m => m.RecipientId == userId && m.RecipientDeleted == false 
             //     && m.SenderId == recipientId 
             //     || m.RecipientId == recipientId && m.SenderId == userId 
             //     && m.SenderDeleted == false)
             // .OrderByDescending(m => m.MessageSent)
             .ToListAsync();

            return departments;
        }

        public async Task<IEnumerable<DepartmentV>> GetDepartmentVs()
        {
            var groups = await _context.DepartmentVs
                 .GroupBy(o => new { o.DepartmentId })
                 .Select(group => new
                 {
                     group.Key.DepartmentId,
                    //  Created = group.Max(x => x.Created),
                    //  Id = group.FirstOrDefault(x=>x.Created == group.Max(z => z.Created)).Id,
                    //  Name = group.FirstOrDefault(x=>x.Created == group.Max(z => z.Created)).Name,
                    depGroup = group.FirstOrDefault(x=>x.Created == group.Max(z => z.Created))
                 })
                 .ToListAsync();

            var listV = new List<DepartmentV>();
            foreach (var group in groups)
            {
                // var depV = new DepartmentV(group.depGroup)
                // {
                //     Id = group.depGroup.Id,

                // }

                    DepartmentV dv = (DepartmentV)group.depGroup;


                listV.Add(dv);
            }


            return listV;
        }
    }
}