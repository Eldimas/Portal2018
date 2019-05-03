using System.Collections.Generic;
using PortalApp.API.Models;

namespace PortalApp.API.Dtos
{
    public class DocumentWfConfig
    {
        public ICollection<string> Ous { get; set; }
        public int Priority { get; set; }
        public WfProcessType ProcessType { get; set; }
        public string Computed { get; set; }
        public bool Editable { get; set; }
        public string Hint { get; set; }
        public bool Required { get; set; }
    }
}