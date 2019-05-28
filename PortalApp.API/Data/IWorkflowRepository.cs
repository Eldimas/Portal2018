using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PortalApp.API.Models;

namespace PortalApp.API.Data
{
    public interface IWorkflowRepository
    {
        Task StartApproval(Guid id);
        WorkflowProcessItem MakeAction(Guid id, string data);
    }
}