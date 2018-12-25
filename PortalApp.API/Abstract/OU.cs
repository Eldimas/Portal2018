using System;
using System.ComponentModel.DataAnnotations;

namespace PortalApp.API.Abstract
{
    public class OU
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string DisplayNameRus { get; set; }
        public string FromNameRus { get; set; }
        public string ToNameRus { get; set; }

        [Required]
        public string DisplayNameEng { get; set; }

        public string FromNameEng { get; set; }

        public string ToNameEng { get; set; }

        public string DisplayNameKaz { get; set; }

        public string FromNameKaz { get; set; }
        public string ToNameKaz { get; set; }


        [Timestamp]
        public Byte[] TimeStamp { get; set; }


        [Required]
        public int Priority { get; set; }

        [Required]
        public bool Disabled { get; set; }
    }
}