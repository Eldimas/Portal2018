using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PortalApp.API.Dtos;

namespace PortalApp.API.Models
{
    public class DocumentConfigVs
    {
        public Guid Id { get; set; }

        public bool NeedRegister{ get; set; }

        public string Category{ get; set; }

        public string TitleGeneration { get; set; }

        [NotMapped]
        public ICollection<DocumentWfConfig> WfConfigsSerialized { get; set; }
        
        [JsonIgnore]
        public string WfConfig 
        {
            get { return JsonConvert.SerializeObject(WfConfigsSerialized); }
            set { WfConfigsSerialized = String.IsNullOrEmpty(value) ? new List<DocumentWfConfig>() : JsonConvert.DeserializeObject<List<DocumentWfConfig>>(value); }
        }
        public ICollection<ContentConfigsSerialized> ContentConfigsSerialized { get; set; }

        public string Author { get; set; }

        public DateTime Created { get; set; }

        public string Title { get; set; }

        public bool ReadOnly { get; set; }

        public bool CopyDocumentFunction { get; set; }

        public bool CloseDocumentFunction { get; set; }

        public bool CreateControlcardFunction { get; set; }
    }
}