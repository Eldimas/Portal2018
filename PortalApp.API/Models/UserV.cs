using System;

namespace PortalApp.API.Models
{
    public class UserV
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

        public int UserId { get; set; }
        public User User { get; set; }
        public Guid DepartmentVId { get; set; }
        public DepartmentV DepartmentV { get; set; }

        public string DeputyUserName { get; set; }
        public string Position { get; set; }
    }
}