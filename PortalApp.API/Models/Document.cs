using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PortalApp.API.Abstract;
using PortalApp.API.Dtos;

namespace PortalApp.API.Models
{
    public class Document: DocumentAbstract
    {
        public string DocumentType { get; set; }
        public string RegNumber { get; set; }
        public DateTime? RegDate { get; set; }
        public ICollection<WorkflowProcessItem> WorkflowProcessItems { get; set; }
        public int Priority { get; set; }

        [ForeignKey("DocumentConfigVs")]
        public Guid DocumentConfigVsId { get; set; }
    }
}