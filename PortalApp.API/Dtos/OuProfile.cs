namespace PortalApp.API.Dtos
{
    public class OuProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayNameEng { get; set; }
        public string DisplayNameRus { get; set; }
        public int Priority { get; set; }
        public object Type { get; set; }
        public string Email { get; set; }
    }
}