using System;

namespace PortalApp.API.Dtos
{
    public class UserVDetailDto
    {
         public Guid Id { get; set; }
        public DateTime Created { get; set; }
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
    }
}