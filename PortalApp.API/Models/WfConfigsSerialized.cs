using System;
using System.Collections.Generic;
using PortalApp.API.Dtos;

namespace PortalApp.API.Models
{
    public class WfConfigsSerialized
    {
        public Guid Id { get; set; }

        public int Priority { get; set; }

        public WfProcessType ProcessType { get; set; }

        public string Computed { get; set; }

        public bool Editable { get; set; }

        public ICollection<UserForListDto> Ous { get; set; }
    }
}