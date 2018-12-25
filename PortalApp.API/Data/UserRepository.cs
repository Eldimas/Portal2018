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

        public async Task<UserForListDto> GetCurrentUser(string name)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x=>x.UserName == name);
            var userForList = new UserForListDto();
            userForList.Id = user.Id;
            userForList.Username = user.UserName;
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
    }
}