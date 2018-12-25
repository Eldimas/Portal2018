using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PortalApp.API.Models
{
    public class DocumentContent
    {
        [Key,ForeignKey("Document")]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Document Document { get; set; }
        [NotMapped]
        public dynamic JSonContent { get; set; }

        [JsonIgnore]
        public string JSonContentString
        {
            get
            {
                return JsonConvert.SerializeObject(JSonContent);
            }
            set
            {
                JSonContent = string.IsNullOrEmpty(value) ? null : JsonConvert.DeserializeObject(value);
            }
        }

        [NotMapped]
        public List<Attachment> Attachments { get; set; }

        [JsonIgnore]
        public string AttachmentsString
        {
            get { return JsonConvert.SerializeObject(Attachments); }
            set { Attachments = string.IsNullOrEmpty(value) ? null : JsonConvert.DeserializeObject<List<Attachment>>(value); }
        }

        [NotMapped]
        public List<Attachment> RefContractAttachments { get; set; }

        [JsonIgnore]
        public string RefContractAttachmentsString
        {
            get { return JsonConvert.SerializeObject(RefContractAttachments); }
            set { RefContractAttachments = string.IsNullOrEmpty(value) ? null : JsonConvert.DeserializeObject<List<Attachment>>(value); }
        }


    }
}