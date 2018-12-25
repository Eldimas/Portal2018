using System;

namespace PortalApp.API.Models
{
    public class UserTemp
    {
       public int Id { get; set; }
       public string Position { get; set; }
       public string DepartmentName { get; set; }
       public string DeputyUserName { get; set; }
       public string Email { get; set; }
       public string Roles { get; set; }
       public string PrefferedCulture { get; set; }
       public string Phone { get; set; }
       public string Mobile { get; set; }
       public string Cabinet { get; set; }
       public string Name { get; set; }
       public string DisplayNameRus { get; set; }
       public string FromNameRus { get; set; }
       public string ToNameRus { get; set; }
       public string DisplayNameEng { get; set; }
       public string FromNameEng { get; set; }
       public string ToNameEng { get; set; }
       public Byte[] TimeStamp { get; set; }
       public int? Priority { get; set; }
       public Boolean? Disabled { get; set; }
       public DateTime? LastLogin { get; set; }
       public string PositionRus { get; set; }
       public string PositionKaz { get; set; }
       public string PositionEng { get; set; }
       public string DisplayNameKaz { get; set; }
       public string FromNameKaz { get; set; }
       public string ToNameKaz { get; set; }
       public string RegionString { get; set; }
       
    }
}