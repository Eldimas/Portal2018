using System;
using System.Collections.Generic;

namespace PortalApp.API.Dtos
{
    public class DocumentConfigVForUpdate
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string TitleGeneration { get; set; }
        public string Author { get; set; }

        //public DateTime Created { get; set; }
        public bool CopyDocumentFunction { get; set; }
        public bool CloseDocumentFunction { get; set; }
        public bool CreateControlcardFunction { get; set; }
        public string Title { get; set; }
        public Guid DocumentConfigId { get; set; }
        public ICollection<DocumentWfConfig> WfConfigsSerialized { get; set; }
        public ICollection<DocumentContentConfigs> ContentConfigsSerialized { get; set; }
    }
}