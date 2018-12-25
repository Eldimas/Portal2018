using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PortalApp.API.Abstract;
using PortalApp.API.Models;

namespace PortalApp.API.Dtos
{
    public class DocumentEditable: DocumentAbstract
    {
        public string DocumentType { get; set; }
        public string RegNumber { get; set; }
        public DateTime? RegDate { get; set; }
        //public List<FrLinkedDocument> LinkedDocuments { get; set; }
        //public List<FrLinkedDocument> LinkedControlCardDocuments { get; set; }
        public DocumentContent Content { get; set; }
        public ICollection<DocumentWfConfig> WfConfigs { get; set; }
        public DocumentStatus Status { get; set; }
        
        [ForeignKey("DocumentConfigVs")]
        public Guid DocumentConfigVsId { get; set; }
    }
}