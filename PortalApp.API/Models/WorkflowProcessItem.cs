using System;
using PortalApp.API.Dtos;

namespace PortalApp.API.Models
{
    public class WorkflowProcessItem
    {
        public Guid Id { get; set; }
        public int Priority { get; set; }
        public UserInfo User { get; set; }
        public string TaskDescription { get; set; }
        public string Comment { get; set; }
        public WfProcessType ProcessType { get; set; }
        public WfProcessResult ProcessResult { get; set; }
        public WfProcessIteration ProcessIteration { get; set; }
        public DateTime Added { get; set; }
        public DateTime Submitted { get; set; }
        public bool Register { get; set; }
        public bool Opened { get; set; }
        public bool Marked { get; set; }
        public string GroupBy { get; set; }
        public Guid DocumentId { get; set; }
        public bool IsSavedInTask { get; set; }
        
    }
}