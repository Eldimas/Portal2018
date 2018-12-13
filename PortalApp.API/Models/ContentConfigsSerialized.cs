using System;

namespace PortalApp.API.Models
{
    public class ContentConfigsSerialized
    {
        public Guid Id { get; set; }

        public string FieldName { get; set; }

        public string FieldDisplayName { get; set; }

        public string Formula { get; set; }

        public string Group { get; set; }

        public string ContentType { get; set; }
    }
}