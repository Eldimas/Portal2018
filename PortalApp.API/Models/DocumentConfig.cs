using System;
using System.Collections.Generic;

namespace PortalApp.API.Models
{
    public class DocumentConfig
    {
        public Guid Id { get; set; }

        public string DocumentType{ get; set; }

        public ICollection<DocumentConfigVs> DocumentConfigVs { get; set; }

    }
}