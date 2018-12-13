using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PortalApp.API.Models
{
    public class User : IdentityUser<int>
    {
        public string PrefferedCulture { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string ImagePath { get; set; }
        public string Cabinet { get; set; }

        public string DisplayNameRus { get; set; }
        public string DisplayNameEng { get; set; }
        public string DisplayNameKaz { get; set; }

        public string FromNameRus { get; set; }
        public string FromNameEng { get; set; }
        public string FromNameKaz { get; set; }

        public string ToNameRus { get; set; }
        public string ToNameEng { get; set; }
        public string ToNameKaz { get; set; }

        public string PositionRus { get; set; }
        public string PositionEng { get; set; }
        public string PositionKaz { get; set; }
        
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        
        public Guid DepartmentVId { get; set; }
        
        public ICollection<UserRole> UserRoles { get; set; }
    }
}