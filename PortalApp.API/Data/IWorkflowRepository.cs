namespace PortalApp.API.Data
{
    public interface IWorkflowRepository
    {
        void StartApproval(Guid id);
        WorkflowProcessItem MakeAction(Guid id, string data);
    }
}