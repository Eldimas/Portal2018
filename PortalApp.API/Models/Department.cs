using System;
using System.Collections.Generic;

namespace PortalApp.API.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string KeyIndex { get; set; }
        public ICollection<DepartmentV> DepartmentVs { get; set; }
    }
}