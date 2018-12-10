using System;
using System.Collections.Generic;

namespace PortalApp.API.Models
{
    public class Region
    {
        public Guid Id { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public string NameKz { get; set; }
        public ICollection<DepartmentV> DepartmentVs { get; set; }
    }
}