using System.Collections.Generic;
using System.Threading.Tasks;
using PortalApp.API.Dtos;
using PortalApp.API.Helpers;
using PortalApp.API.Models;

namespace PortalApp.API.Data
{
    public interface IUserRepository
    {
         
         Task<IEnumerable<User>> GetAllUsers();
         Task<UserForListDto> GetCurrentUser(string name);
         Task<UserProfile> GetCurrentUserProfile(string name);
    }
}