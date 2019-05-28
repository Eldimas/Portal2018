using System;

namespace PortalApp.API.Dtos
{
    public class WfProcessInfo
    {
        public bool NeedRegister { get; set; }
        public DateTime Started { get; set; }
        public DateTime Ended { get; set; }
        
    }
}