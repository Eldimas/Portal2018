using System;
using System.ComponentModel.DataAnnotations;
using PortalApp.API.Dtos;

namespace PortalApp.API.Abstract
{
    public abstract class DocumentAbstract
    {
        public Guid Id { get; set; }

        [Required]
        public UserForListDto Author { get; set; }

        [Required]
        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Timestamp]
        public Byte[] TimeStamp { get; set; }

        public bool ReadOnly { get; set; }
        public bool Deleted { get; set; }

        public DateTime? Control { get; set; }
    }
}