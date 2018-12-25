using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PortalApp.API.Abstract;

namespace PortalApp.API.Dtos
{
    public class UserProfile: OU
    {
        public string Position { get; set; }

        public string PositionRus { get; set; }

        public string PositionEng { get; set; }

        public string PositionKaz { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        public string DeputyUserName { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public List<string> Roles { get; set; }

        [Column("Roles")]
        public string RolesString
        {
            get { return JsonConvert.SerializeObject(Roles); }
            set { Roles = string.IsNullOrEmpty(value) ? null : JsonConvert.DeserializeObject<List<string>>(value); }
        }

        [NotMapped]
        public List<string> Region { get; set; }

        [Column("RegionString")]
        public string RegionString
        {
            get { return JsonConvert.SerializeObject(Region); }
            set { Region = string.IsNullOrEmpty(value) ? null : JsonConvert.DeserializeObject<List<string>>(value); }
        }
        public string PrefferedCulture { get; set; }
        public Guid ImagePath { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Cabinet { get; set; }
        public DateTime LastLogin { get; set; }
    }
}