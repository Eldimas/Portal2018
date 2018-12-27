using System.Collections.Generic;

namespace PortalApp.API.Models
{
    public class Navig
    {
         public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        
        public ICollection<Navig> Children { get; set; }
    }
}