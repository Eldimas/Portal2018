using System;
using System.Collections.Generic;

namespace PortalApp.API.Dtos
{
    public class UserDetailsDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public ICollection<UserVDetailDto> UserVs { get; set; }
    }
}