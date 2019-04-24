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

        [NotMapped]
        public ICollection<DocumentContentConfigs> ContentConfigsSerialized { get; set; }

        [JsonIgnore]
        public string ContentConfigs 
        {
            get { return JsonConvert.SerializeObject(ContentConfigsSerialized); }
            set { ContentConfigsSerialized = String.IsNullOrEmpty(value) ? new List<DocumentContentConfigs>() : JsonConvert.DeserializeObject<List<DocumentContentConfigs>>(value); }
        }
        public string Author { get; set; }

        public DateTime Created { get; set; }

        public string Title { get; set; }

        public bool ReadOnly { get; set; }

        public bool CopyDocumentFunction { get; set; }

        public bool CloseDocumentFunction { get; set; }

        public bool CreateControlcardFunction { get; set; }
        [ForeignKey("DocumentConfig")]
        public Guid DocumentConfigId { get; set; }
    }
}