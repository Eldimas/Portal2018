using System;
using System.Collections.Generic;
using PortalApp.API.Dtos;

namespace PortalApp.API.Models
{
    public class DocumentConfigVs
    {
        public Guid Id { get; set; }

        public bool NeedRegister{ get; set; }

        public string Category{ get; set; }

        public string TitleGeneration { get; set; }

        public ICollection<DocumentWfConfig> WfConfigsSerialized { get; set; }

        public ICollection<ContentConfigsSerialized> ContentConfigsSerialized { get; set; }

        public User Author { get; set; }

        public DateTime Created { get; set; }

        public string Title { get; set; }

        public bool ReadOnly { get; set; }

        public bool CopyDocumentFunction { get; set; }

        public bool CloseDocumentFunction { get; set; }

        public bool CreateControlcardFunction { get; set; }
    }
}