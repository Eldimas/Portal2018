using System;

namespace PortalApp.API.Models
{
    public class Attachment
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Added { get; set; }
        public long Size { get; set; }
        public string TypeFile { get; set; }
    }
}