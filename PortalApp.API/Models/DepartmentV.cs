using System;
using System.Collections.Generic;

namespace PortalApp.API.Models
{
    public class DepartmentV
    {
        
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        // public string Author { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public string DisplayNameRus { get; set; }
        public string DisplayNameEng { get; set; }
        public string DisplayNameKaz { get; set; }

        public string FromNameRus { get; set; }
        public string FromNameEng { get; set; }
        public string FromNameKaz { get; set; }


        public string ToNameRus { get; set; }
        public string ToNameEng { get; set; }
        public string ToNameKaz { get; set; }

        public int Priority { get; set; }
        public bool Disabled { get; set; }
        
        public Guid RegionId { get; set; }
        public ICollection<User> Users { get; set; }

        public Guid DepartmentId { get; set; }
    }
}