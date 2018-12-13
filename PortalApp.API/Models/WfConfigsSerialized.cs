using System;
using System.Collections.Generic;

namespace PortalApp.API.Models
{
    public class WfConfigsSerialized
    {
        public Guid Id { get; set; }

        public int Priority { get; set; }

        public int ProcessType { get; set; }

        public string Computed { get; set; }

        public bool Editable { get; set; }

        public ICollection<User> Ous { get; set; }
    }
}