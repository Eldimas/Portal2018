using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalApp.API.Dtos
{
    public class WfProcessInfo
    {
        public Guid Id { get; set; }
        public bool NeedRegister { get; set; }
        public DateTime Started { get; set; }
        public DateTime Ended { get; set; }
        
    }
}