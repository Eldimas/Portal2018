using System;
using System.Collections.Generic;

namespace PortalApp.API.Models
{
    public class DocumentConfigV
    {
        public Guid Id { get; set; }

        public bool NeedRegister{ get; set; }

        public string Category{ get; set; }

        public string TitleGeneration { get; set; }

        public ICollection<WfConfigsSerialized> WfConfigsSerialized { get; set; }

        public ICollection<ContentConfigsSerialized> ContentConfigsSerialized { get; set; }

        public ICollection<User> Author { get; set; }

        public DateTime Created { get; set; }

        public string Title { get; set; }

        public bool ReadOnly { get; set; }

        public bool CopyDocumentFunction { get; set; }

        public bool CloseDocumentFunction { get; set; }

        public bool CreateControlcardFunction { get; set; }
    }
}