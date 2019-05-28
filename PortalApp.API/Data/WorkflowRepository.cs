namespace PortalApp.API.Data
{
    public class WorkflowRepository: IWorkflowRepository
    {
        public void StartApproval(Guid id)
        {
            using (var context = new DoczContext())
            {
                var doc = Load(context, id);

                var config = context.DocumentConfigs.SingleOrDefault(x => x.DocumentType == doc.DocumentType);
                if (config == null) throw (new Exception(String.Format("error: document with id={0} has no config", id)));

                if (config.RegisterAtSave && string.IsNullOrEmpty(doc.RegNumber))
                {
                    var max = context.Documents.Count(x => x.DocumentType == doc.DocumentType && x.Status != DocumentStatus.Draft) + 1;
                    doc.RegNumber = max.ToString();
                    doc.RegDate = DateTime.Now;
                }

                Prepare(doc, config);

                var u = GenerateNextStep(context, doc.Id);
                context.SaveChanges();
            }
        }
        private void Prepare(Document doc, DocumentConfig config)
        {
            if (doc.WfInfo == null) doc.WfInfo = new WfProcessInfo();

            doc.WfInfo.Started = DateTime.Now;
            doc.WfInfo.NeedRegister = config.NeedRegister;
            doc.PersentCompleted = 0;
            doc.ReadOnly = true;
            doc.Status = DocumentStatus.OnApproval;


            var regsTrueCount = doc.WorkflowProcess.Where(x => x.Register).Count();
            var maxRegPriority = 0;
            if (regsTrueCount > 0)
            {
                maxRegPriority=doc.WorkflowProcess.Where(x => x.Register).Max(x => x.Priority);
            }

            
            var notRegistered = doc.WorkflowProcess.Where(x => x.Register == false);
            
            foreach (var wf in notRegistered)
            {
                if (wf.Priority < maxRegPriority)
                {
                    wf.Priority = maxRegPriority + 1;
                }

            }

            foreach (var wf in doc.WorkflowProcess.Where(wf => wf.Id == Guid.Empty))
            {
                wf.Id = Guid.NewGuid();
            }

            doc.WorkflowProcess = doc.WorkflowProcess.OrderBy(x => x.Priority).ToList();
        }

        private List<WorkflowProcessItem> GenerateNextStep(DoczContext context, Guid docId)
        {
            List<WorkflowProcessItem> deputyList = new List<WorkflowProcessItem>();
            var inUse = context.WorkflowProcessItems.Count(
                x => x.ProcessIteration == WfProcessIteration.Started 
                && x.DocumentId == docId 
                && x.ProcessType != WfProcessType.Copy 
                && x.ProcessType !=WfProcessType.Supervisor
                && x.ProcessType != WfProcessType.ForInformation
                && x.ProcessType != WfProcessType.Informed
                && x.ProcessType != WfProcessType.Assignment
                && x.ProcessType != WfProcessType.CopyForRegistration
                );

            var inUseList = context.WorkflowProcessItems.Where(
                x => x.ProcessIteration == WfProcessIteration.Started
                && x.DocumentId == docId
                && x.ProcessType != WfProcessType.Copy
                && x.ProcessType != WfProcessType.Supervisor
                && x.ProcessType != WfProcessType.ForInformation
                && x.ProcessType != WfProcessType.Informed
                && x.ProcessType != WfProcessType.Assignment
                && x.ProcessType != WfProcessType.CopyForRegistration
                );

            if (inUse > 0)
                return null;

            var reg = context.WorkflowProcessItems
                .FirstOrDefault(x => x.DocumentId == docId && x.ProcessType == WfProcessType.Registration);

            


            var regStatus = false;
            if(reg !=null && (reg.ProcessResult == WfProcessResult.Ended 
                || reg.ProcessResult == WfProcessResult.Appruved))
            {
                regStatus = true;
            }
            int min = 0;
            int minCopy = 0;

            var wfitems = context.WorkflowProcessItems.Where(
                    x => x.ProcessIteration == WfProcessIteration.NotStarted
                    && x.DocumentId == docId);

            var minList = wfitems.Where( 
                    x => x.ProcessType != WfProcessType.Supervisor
                    && x.ProcessType != WfProcessType.Copy
                    && x.ProcessType != WfProcessType.ForInformation
                    && x.ProcessType != WfProcessType.Informed
                    && x.ProcessType != WfProcessType.Control
                    && x.ProcessType != WfProcessType.CopyForRegistration
                    ).ToList();
            if(minList.Count > 0)
            {
                min = minList.Min(x => x.Priority);
            }

            var minCopyList = new List<WorkflowProcessItem>();
            if( min > 0)
            {
                minCopyList = wfitems.Where(
                   x => x.Priority < min
                  && (x.ProcessType == WfProcessType.Supervisor
                  || x.ProcessType == WfProcessType.Copy
                  || x.ProcessType == WfProcessType.ForInformation
                  || x.ProcessType == WfProcessType.Informed
                  || x.ProcessType == WfProcessType.Control
                  || x.ProcessType == WfProcessType.CopyForRegistration
                  )).ToList();
                if (minCopyList.Count > 0)
                {
                    minCopy = minCopyList.Min(x => x.Priority);
                }
            }
           
            if (regStatus)
            {
                minCopyList.AddRange(wfitems.Where(
                    x=>x.Priority > reg.Priority
                   && (x.ProcessType == WfProcessType.Supervisor
                   || x.ProcessType == WfProcessType.Copy
                   || x.ProcessType == WfProcessType.ForInformation
                   || x.ProcessType == WfProcessType.Informed
                   || x.ProcessType == WfProcessType.Control
                   || x.ProcessType == WfProcessType.CopyForRegistration
                   )).ToList());
                if (minCopyList.Count > 0)
                {
                    minCopy = minCopyList.Min(x => x.Priority);
                }
            }
           

            var currentDoc = context.Documents.FirstOrDefault(x => x.Id == docId);
            if (currentDoc.Status == DocumentStatus.Rejected)
            {
                return null;
            }
            var author = currentDoc?.Author;

            var isMemoDoc = currentDoc.DocumentType== "Memo";

            var notStarted = new List<WorkflowProcessItem>();


            if (min > 0 || minCopy > 0)
            {
                if(min > 0)
                {
                    notStarted.AddRange(context.WorkflowProcessItems
                    .Where(x => x.ProcessIteration == WfProcessIteration.NotStarted
                        && x.DocumentId == docId && x.Priority == min).ToList());

                    var wfReg = context.WorkflowProcessItems
                    .FirstOrDefault(x => x.ProcessIteration == WfProcessIteration.NotStarted
                        && x.DocumentId == docId && x.Priority == min);

                    if (wfReg!=null && wfReg.ProcessType == WfProcessType.Registration)
                    {
                        currentDoc.Status= DocumentStatus.OnRegistration;
                        context.SaveChanges();
                    }
                }

                if (minCopy > 0)
                {
                    notStarted.AddRange(minCopyList);
                }



                var control = context.Documents
                    .Where(x => x.Id == docId).Select(x => x.Control).First();
              

                foreach (var wf in notStarted)
                {
                    if (wf.Id == Guid.Empty)
                        wf.Id = Guid.NewGuid();
                    wf.Added = DateTime.Now;
                    wf.ProcessIteration = WfProcessIteration.Started;
                    wf.ControlDate = control != null && control > DateTime.MinValue ? control : null;


                    var wfParent = wf;
                    var deputyUser = context.UserProfiles
                        .SingleOrDefault(x => x.Name == wf.User.Name);


                    var userList = new List<UserInfo>();
                    var isUserReply = false;

                    while (!string.IsNullOrEmpty(deputyUser?.DeputyUserName))
                    {


                        var wfdeputy =
                            context.WorkflowProcessItems.FirstOrDefault(
                                x => x.DocumentId == docId
                                && x.User.Name == deputyUser.Name
                                && x.Id== wfParent.Id
                                && x.Priority == wf.Priority);


                        var depUser = context.UserProfiles
                            .SingleOrDefault(x => x.Name == deputyUser.DeputyUserName);


                        userList.Add(wfdeputy.User);
                        
                        foreach (var user in userList)
                        {
                            if (user.Name == deputyUser.DeputyUserName)
                            {
                                isUserReply = true;
                            }
                        }


                        if (depUser?.DeputyUserName != deputyUser.Name && isUserReply!=true)
                        {
                            deputyUser = depUser;
                            var checkForReply = false;
                            foreach (var user in userList)
                            {
                                if (user.Name == deputyUser.DeputyUserName)
                                {
                                    checkForReply = true;
                                }
                            }
                            if (checkForReply)
                            {
                                wf.Submited = DateTime.Now;
                                wf.ProcessIteration = WfProcessIteration.Ended;
                                wf.ProcessResult = WfProcessResult.Redirected;

                                var userFromDeputy = new WorkflowProcessItem
                                {
                                    Id = Guid.NewGuid(),
                                    Priority = wf.Priority,
                                    User =
                                        new UserInfo
                                        {
                                            DepartmentName = deputyUser?.DepartmentName,
                                            Name = deputyUser?.Name,
                                            Position = deputyUser?.Position
                                        },

                                    ProcessType = wf.ProcessType,
                                    ProcessResult = WfProcessResult.Unknown,
                                    ProcessIteration = WfProcessIteration.Started,
                                    Added = DateTime.Now,
                                    Register = wf.Register,
                                    GroupBy = wf.GroupBy,
                                    ControlDate = wf.ControlDate,
                                    DocumentId = wf.DocumentId,
                                    RefBy = new List<WorkflowProcessItem> { wfdeputy },
                                    Submited = null
                                };

                                context.WorkflowProcessItems.Add(userFromDeputy);
                                context.SaveChanges();
                                deputyList.Add(userFromDeputy);
                                break;
                            }


                            if (!string.IsNullOrEmpty(deputyUser?.DeputyUserName) && !checkForReply)
                            {
                                var userFromDeputy = new WorkflowProcessItem
                                {
                                    Id = Guid.NewGuid(),
                                    Priority = wf.Priority,
                                    User =
                                        new UserInfo
                                        {
                                            DepartmentName = deputyUser.DepartmentName,
                                            Name = deputyUser.Name,
                                            Position = deputyUser.Position
                                        },

                                    ProcessType = wf.ProcessType,
                                    ProcessResult = WfProcessResult.Redirected,
                                    ProcessIteration = WfProcessIteration.Ended,
                                    Added = DateTime.Now,
                                    Register = wf.Register,
                                    GroupBy = wf.GroupBy,
                                    ControlDate = wf.ControlDate,
                                    DocumentId = wf.DocumentId,
                                    RefBy = new List<WorkflowProcessItem> { wfdeputy },
                                    Submited=DateTime.Now
                                };

                                context.WorkflowProcessItems.Add(userFromDeputy);
                                context.SaveChanges();
                                deputyList.Add(userFromDeputy);

                                wfParent = userFromDeputy;
                            }
                            else
                            {
                                wf.Submited = DateTime.Now;
                                wf.ProcessIteration = WfProcessIteration.Ended;
                                wf.ProcessResult = WfProcessResult.Redirected;

                                var userFromDeputy = new WorkflowProcessItem
                                {
                                    Id = Guid.NewGuid(),
                                    Priority = wf.Priority,
                                    User =
                                        new UserInfo
                                        {
                                            DepartmentName = deputyUser?.DepartmentName,
                                            Name = deputyUser?.Name,
                                            Position = deputyUser?.Position
                                        },

                                    ProcessType = wf.ProcessType,
                                    ProcessResult = WfProcessResult.Unknown,
                                    ProcessIteration = WfProcessIteration.Started,
                                    Added = DateTime.Now,
                                    Register = wf.Register,
                                    GroupBy = wf.GroupBy,
                                    ControlDate = wf.ControlDate,
                                    DocumentId = wf.DocumentId,
                                    RefBy = new List<WorkflowProcessItem> { wfdeputy },
                                    Submited=null
                                };

                                context.WorkflowProcessItems.Add(userFromDeputy);
                                deputyList.Add(userFromDeputy);
                            }
                        }
                    }
                 }

                if (deputyList.Count > 0)
                {
                    notStarted.AddRange(deputyList);
                }

                return notStarted.ToList();
            }

            return null;
        }
    }
}