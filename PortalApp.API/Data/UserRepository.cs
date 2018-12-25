using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.Dtos;
using PortalApp.API.Helpers;
using PortalApp.API.Models;

namespace PortalApp.API.Data
{
    public class UserRepository : IUserRepository
    {
        public readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<OUForListDto> GetCurrentUser(string name)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x=>x.UserName == name);
            var userForList = new OUForListDto();
            //userForList.Id = user.Id;
            userForList.Name = user.UserName;
            return userForList;
        }

        public async Task<UserProfile> GetCurrentUserProfile(string name)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x=>x.UserName == name);
            var userForList = new UserProfile();
            userForList.Id = user.Id;
            userForList.Name = user.UserName;
            return userForList;
        }

        public async Task<OUForListDto> GetCurrentUsersDepartment(string name)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x=>x.UserName == name);
            Guid depId = Guid.Empty;
            foreach(var u in user.UserVs){
                depId = u.DepartmentVId;
            }
            var dep = await _context.DepartmentVs.SingleOrDefaultAsync(x=>x.Id == depId);
            var depForList = new OUForListDto();
            depForList.Id = dep.Id;
            depForList.Name = dep.Name;
            return depForList;
        }

    }
}