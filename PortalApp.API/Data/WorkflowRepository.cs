using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PortalApp.API.Models;
using System.Linq;
using PortalApp.API.Dtos;
using Newtonsoft.Json;

namespace PortalApp.API.Data
{
    public class WorkflowRepository: IWorkflowRepository
    {
        private readonly DataContext _context;
        public async Task StartApproval(Guid id)
        {
            var doc = _context.Documents.SingleOrDefault(x => x.Id == id);

            var config = _context.DocumentConfigs.SingleOrDefault(x=>x.DocumentType == doc.DocumentType);
            if (config == null) throw (new Exception(String.Format("error: document with id={0} has no config", id)));

            if (string.IsNullOrEmpty(doc.RegNumber))
            {
                var max = _context.Documents.Count(x => x.DocumentType == doc.DocumentType && x.Status != DocumentStatus.Draft) + 1;
                doc.RegNumber = max.ToString();
                doc.RegDate = DateTime.Now;
            }

            Prepare(doc, config);

            var u = GenerateNextStep(doc.Id);
            _context.SaveChanges();
        }
        private void Prepare(Document doc, DocumentConfig config)
        {
            if (doc.WfInfo == null) doc.WfInfo = new WfProcessInfo();

            doc.WfInfo.Started = DateTime.Now;
            doc.WfInfo.NeedRegister = config.DocumentConfigVs.SingleOrDefault(x=>x.Id == doc.DocumentConfigVsId).NeedRegister;
            doc.ReadOnly = true;
            doc.Status = DocumentStatus.OnApproval;

            var regsTrueCount = doc.WorkflowProcessItems.Where(x => x.Register).Count();
            var maxRegPriority = 0;
            if (regsTrueCount > 0)
            {
                maxRegPriority=doc.WorkflowProcessItems.Where(x => x.Register).Max(x => x.Priority);
            }

            
            var notRegistered = doc.WorkflowProcessItems.Where(x => x.Register == false);
            
            foreach (var wf in notRegistered)
            {
                if (wf.Priority < maxRegPriority)
                {
                    wf.Priority = maxRegPriority + 1;
                }

            }

            foreach (var wf in doc.WorkflowProcessItems.Where(wf => wf.Id == Guid.Empty))
            {
                wf.Id = Guid.NewGuid();
            }

            doc.WorkflowProcessItems = doc.WorkflowProcessItems.OrderBy(x => x.Priority).ToList();
        }

    public WorkflowProcessItem MakeAction(Guid id, string data)
        {
            var item = _context.WorkflowProcessItems.SingleOrDefault(x => x.Id == id);
            if (item == null) return null;
            dynamic d = JsonConvert.DeserializeObject(data);
            bool stop = false;

            switch (item.ProcessType)
            {
                case WfProcessType.Action:
                    Action(item, d);
                    break;
                case WfProcessType.Approval:
                    stop = Approve(item, d);
                    break;
                case WfProcessType.Consulting:
                    Read(item);
                    break;
                case WfProcessType.Copy:
                    Read(item);
                    break;
                case WfProcessType.ForInformation:
                    Read(item);
                    break;
                case WfProcessType.Control:
                    Read(item);
                    break;
                case WfProcessType.FormatApproval:
                    stop = Approve(item, d);
                    break;
                case WfProcessType.Informed:
                    Read(item);
                    break;
                case WfProcessType.Reciever:
                    if (item.Register)
                    {
                        stop = Approve(item, d);
                    }
                    else {
                        Read(item);
                    } 
                    //stop = Approve(context, item, d);
                    break;
                case WfProcessType.Registration:
                    stop = Register(item, d);
                    break;
                case WfProcessType.Sender:
                    stop = ApproveSender(item, d);
                    break;
                case WfProcessType.Supervisor:
                    Read(item);
                    break;
                case WfProcessType.TopApproval:
                    stop = Approve(item, d);
                    break;
                case WfProcessType.CopyForRegistration:
                    Read(item);
                    break;
                default:
                    stop = Approve(item, d);
                    break;
            }

            _context.SaveChanges();

            if (!stop)
            {
                var u = GenerateNextStep(item.DocumentId);
                _context.SaveChanges();

                if (u != null && u.Count > 0)
                {
                    var docId = u.First().DocumentId;
                    var title = _context.Documents.Where(x => x.Id == docId)
                        .Select(x => new { Title = x.Title, DocType = x.DocumentType })
                        .SingleOrDefault();
                }

                if (item.ProcessType == WfProcessType.Approval)
                {
                    var allApproved = _context.WorkflowProcessItems.Where(x => x.DocumentId == item.DocumentId 
                    && x.ProcessType == WfProcessType.Approval 
                    && x.Id != item.Id 
                    && x.ProcessIteration == WfProcessIteration.Started).ToList(); //&& x.Id != item.Id
                    if (allApproved.Count == 0)
                    {
                        var doc = _context.Documents.FirstOrDefault(x => x.Id == item.DocumentId);
                        if (doc != null)
                        {
                            doc.Status = DocumentStatus.Approved;
                            _context.SaveChanges();
                        }

                    }
                }

                if (_context.WorkflowProcessItems
                    .Where(x => x.DocumentId == item.DocumentId)
                    .All(x => x.ProcessIteration == WfProcessIteration.Ended))
                {
                    var doc = _context.Documents.Single(x => x.Id == item.DocumentId);
                    doc.Status = DocumentStatus.Ended;

                    _context.SaveChanges();
                }
            }

            

            return item;

        }

        private bool Approve(WorkflowProcessItem item, dynamic data)
        {
            item.ProcessIteration = WfProcessIteration.Ended;
            item.Submitted = DateTime.Now;
            if (data != null)
            {
                item.ProcessResult = (bool)data.Approve ? WfProcessResult.Appruved : WfProcessResult.Rejected;
                item.Comment = (string)data.Comment;
            }
            

            //var allApproved= context.WorkflowProcessItems.Where(x => x.DocumentId == item.DocumentId && x.Id != item.Id && x.ProcessIteration == WfProcessIteration.Started);



            if (item.ProcessResult == WfProcessResult.Rejected)
            {
                var started = _context.WorkflowProcessItems.Where(x => x.DocumentId == item.DocumentId && x.Id != item.Id && x.ProcessIteration == WfProcessIteration.Started);
                foreach (var wf in started)
                {
                    wf.ProcessIteration = WfProcessIteration.NotStarted;
                }

                var doc = _context.Documents.Single(x => x.Id == item.DocumentId);
                doc.Status = DocumentStatus.Rejected;
                return true;
            }

            return false;
        }

        private bool ApproveReciever(WorkflowProcessItem item, dynamic data)
        {
            //item.ProcessIteration = WfProcessIteration.Ended;
            item.Submitted = DateTime.Now;
            if (data != null)
            {
                item.ProcessResult = (bool)data.Approve ? WfProcessResult.Appruved : WfProcessResult.Rejected;
                item.Comment = (string)data.Comment;
            }


            //var allApproved= context.WorkflowProcessItems.Where(x => x.DocumentId == item.DocumentId && x.Id != item.Id && x.ProcessIteration == WfProcessIteration.Started);



            if (item.ProcessResult == WfProcessResult.Rejected)
            {
                var started = _context.WorkflowProcessItems.Where(x => x.DocumentId == item.DocumentId && x.Id != item.Id && x.ProcessIteration == WfProcessIteration.Started);
                foreach (var wf in started)
                {
                    wf.ProcessIteration = WfProcessIteration.NotStarted;
                }

                var doc = _context.Documents.Single(x => x.Id == item.DocumentId);
                doc.Status = DocumentStatus.Rejected;
                return true;
            }

            return false;
        }


        private bool ApproveSender(WorkflowProcessItem item, dynamic data)
        {
            item.ProcessIteration = WfProcessIteration.Ended;
            item.ProcessResult = (bool)data.Approve ? WfProcessResult.Appruved : WfProcessResult.Rejected;
            item.Submitted = DateTime.Now;
            item.Comment = (string)data.Comment;

            //var allApproved= context.WorkflowProcessItems.Where(x => x.DocumentId == item.DocumentId && x.Id != item.Id && x.ProcessIteration == WfProcessIteration.Started);



            if (item.ProcessResult == WfProcessResult.Rejected)
            {
                var started = _context.WorkflowProcessItems.Where(x => x.DocumentId == item.DocumentId && x.Id != item.Id && x.ProcessIteration == WfProcessIteration.Started);
                foreach (var wf in started)
                {
                    wf.ProcessIteration = WfProcessIteration.NotStarted;
                }

                var doc = _context.Documents.Single(x => x.Id == item.DocumentId);
                if (doc.DocumentType == "MemoForPay_RecieverApprove")
                {
                    doc.Status = DocumentStatus.Draft;
                    doc.ReadOnly = false;
                    return true;
                }
                else
                {
                    doc.Status = DocumentStatus.Rejected;
                    return true;
                }

            }

            return false;
        }

        private bool Action(WorkflowProcessItem item, dynamic data)
        {
            if ((string)data.Action == "ended")
            {
                item.ProcessIteration = WfProcessIteration.Ended;
                item.ProcessResult = WfProcessResult.Ended;
                item.Submitted = DateTime.Now;
                item.Comment += (string)data.Comment;
            }
            else if ((string)data.Action == "cancelled")
            {
                item.ProcessIteration = WfProcessIteration.Ended;
                item.ProcessResult = WfProcessResult.Rejected;
                item.Submitted = DateTime.Now;
                item.Comment += (string)data.Comment;
            }
            else if ((string)data.Action == "inwork")
            {
                item.ProcessIteration = WfProcessIteration.Started;
                item.ProcessResult = WfProcessResult.InWork;
            }

            return false;
        }

        private void Read(WorkflowProcessItem item)
        {
            item.ProcessIteration = WfProcessIteration.Ended;
            item.ProcessResult = WfProcessResult.Readed;
            item.Submitted = DateTime.Now;
        }

        private bool Register(WorkflowProcessItem item, dynamic data)
        {
            item.ProcessIteration = WfProcessIteration.Ended;
            item.ProcessResult = (bool)data.Approve ? WfProcessResult.Appruved : WfProcessResult.Rejected;
            item.Submitted = DateTime.Now;
            item.Comment = (string)data.Comment;

            var doc = _context.Documents.Single(x => x.Id == item.DocumentId);

            if (item.ProcessResult == WfProcessResult.Rejected)
            {
                doc.Status = DocumentStatus.Rejected;
                return true;
            }
            else
            {
                doc.RegNumber = (string)data.RegNumber;
                //string strDate = data.RegDate;
                //strDate = strDate.Replace(".", "/");
                //var conv =DateTime.ParseExact(strDate, "dd/MM/yyyy", null);

                //var conv = DateTime.ParseExact((string)data.RegDate, "dd.MM.yyyy", null);

                var src = DateTime.Now;
                var hm = (src.Hour.ToString().Length < 2 ? "0" + src.Hour : src.Hour.ToString()) + ":" + (src.Minute.ToString().Length < 2 ? "0" + src.Minute : src.Minute.ToString()) + ":" + (src.Second.ToString().Length < 2 ? "0" + src.Second : src.Second.ToString());
                data.RegDate = data.RegDate + " " + hm;

                //var dt = "11.01.2017 09:40:33";
                //var dtz = DateTime.ParseExact((string)dt, "dd.MM.yyyy HH:mm:ss", null);

                doc.RegDate = DateTime.ParseExact((string)data.RegDate, "dd.MM.yyyy HH:mm:ss", null);
                doc.Status = DocumentStatus.Registered;
                return false;
            }
        }
        private List<WorkflowProcessItem> GenerateNextStep(Guid docId)
        {
            List<WorkflowProcessItem> deputyList = new List<WorkflowProcessItem>();
            var inUse = _context.WorkflowProcessItems.Count(
                x => x.ProcessIteration == WfProcessIteration.Started 
                && x.DocumentId == docId 
                && x.ProcessType != WfProcessType.Copy 
                && x.ProcessType !=WfProcessType.Supervisor
                && x.ProcessType != WfProcessType.ForInformation
                && x.ProcessType != WfProcessType.Informed
                && x.ProcessType != WfProcessType.Assignment
                && x.ProcessType != WfProcessType.CopyForRegistration
                );

            var inUseList = _context.WorkflowProcessItems.Where(
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

            var reg = _context.WorkflowProcessItems
                .FirstOrDefault(x => x.DocumentId == docId && x.ProcessType == WfProcessType.Registration);

            


            var regStatus = false;
            if(reg !=null && (reg.ProcessResult == WfProcessResult.Ended 
                || reg.ProcessResult == WfProcessResult.Appruved))
            {
                regStatus = true;
            }
            int min = 0;
            int minCopy = 0;

            var wfitems = _context.WorkflowProcessItems.Where(
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
           

            var currentDoc = _context.Documents.FirstOrDefault(x => x.Id == docId);
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
                    notStarted.AddRange(_context.WorkflowProcessItems
                    .Where(x => x.ProcessIteration == WfProcessIteration.NotStarted
                        && x.DocumentId == docId && x.Priority == min).ToList());

                    var wfReg = _context.WorkflowProcessItems
                    .FirstOrDefault(x => x.ProcessIteration == WfProcessIteration.NotStarted
                        && x.DocumentId == docId && x.Priority == min);

                    if (wfReg!=null && wfReg.ProcessType == WfProcessType.Registration)
                    {
                        currentDoc.Status= DocumentStatus.OnRegistration;
                        _context.SaveChanges();
                    }
                }

                if (minCopy > 0)
                {
                    notStarted.AddRange(minCopyList);
                }



                var control = _context.Documents
                    .Where(x => x.Id == docId).Select(x => x.Control).First();
              

                foreach (var wf in notStarted)
                {
                    if (wf.Id == Guid.Empty)
                        wf.Id = Guid.NewGuid();
                    wf.Added = DateTime.Now;
                    wf.ProcessIteration = WfProcessIteration.Started;
                    // wf.ControlDate = control != null && control > DateTime.MinValue ? control : null;


                    var wfParent = wf;
                    var deputyUser = _context.UserVs
                        .SingleOrDefault(x => x.User.UserName == wf.User.Name);


                    var userList = new List<UserInfo>();
                    var isUserReply = false;

                    while (!string.IsNullOrEmpty(deputyUser?.DeputyUserName))
                    {


                        var wfdeputy =
                            _context.WorkflowProcessItems.FirstOrDefault(
                                x => x.DocumentId == docId
                                && x.User.Name == deputyUser.User.UserName
                                && x.Id== wfParent.Id
                                && x.Priority == wf.Priority);


                        var depUser = _context.UserVs
                            .SingleOrDefault(x => x.User.UserName == deputyUser.DeputyUserName);


                        userList.Add(wfdeputy.User);
                        
                        foreach (var user in userList)
                        {
                            if (user.Name == deputyUser.DeputyUserName)
                            {
                                isUserReply = true;
                            }
                        }


                        if (depUser?.DeputyUserName != deputyUser.User.UserName && isUserReply!=true)
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
                                wf.Submitted = DateTime.Now;
                                wf.ProcessIteration = WfProcessIteration.Ended;
                                wf.ProcessResult = WfProcessResult.Redirected;

                                var userFromDeputy = new WorkflowProcessItem
                                {
                                    Id = Guid.NewGuid(),
                                    Priority = wf.Priority,
                                    User =
                                        new UserInfo
                                        {
                                            DepartmentName = deputyUser?.DepartmentV.Name,
                                            Name = deputyUser?.User.UserName,
                                            Position = deputyUser?.Position
                                        },

                                    ProcessType = wf.ProcessType,
                                    ProcessResult = WfProcessResult.Unknown,
                                    ProcessIteration = WfProcessIteration.Started,
                                    Added = DateTime.Now,
                                    Register = wf.Register,
                                    GroupBy = wf.GroupBy,
                                    DocumentId = wf.DocumentId,
                                    RefBy = new List<WorkflowProcessItem> { wfdeputy },
                                    Submitted = null
                                };

                                _context.WorkflowProcessItems.Add(userFromDeputy);
                                _context.SaveChanges();
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
                                            DepartmentName = deputyUser.DepartmentV.Name,
                                            Name = deputyUser.User.UserName,
                                            Position = deputyUser.Position
                                        },

                                    ProcessType = wf.ProcessType,
                                    ProcessResult = WfProcessResult.Redirected,
                                    ProcessIteration = WfProcessIteration.Ended,
                                    Added = DateTime.Now,
                                    Register = wf.Register,
                                    GroupBy = wf.GroupBy,
                                    DocumentId = wf.DocumentId,
                                    RefBy = new List<WorkflowProcessItem> { wfdeputy },
                                    Submitted=DateTime.Now
                                };

                                _context.WorkflowProcessItems.Add(userFromDeputy);
                                _context.SaveChanges();
                                deputyList.Add(userFromDeputy);

                                wfParent = userFromDeputy;
                            }
                            else
                            {
                                wf.Submitted = DateTime.Now;
                                wf.ProcessIteration = WfProcessIteration.Ended;
                                wf.ProcessResult = WfProcessResult.Redirected;

                                var userFromDeputy = new WorkflowProcessItem
                                {
                                    Id = Guid.NewGuid(),
                                    Priority = wf.Priority,
                                    User =
                                        new UserInfo
                                        {
                                            DepartmentName = deputyUser?.DepartmentV.Name,
                                            Name = deputyUser?.User.UserName,
                                            Position = deputyUser?.Position
                                        },

                                    ProcessType = wf.ProcessType,
                                    ProcessResult = WfProcessResult.Unknown,
                                    ProcessIteration = WfProcessIteration.Started,
                                    Added = DateTime.Now,
                                    Register = wf.Register,
                                    GroupBy = wf.GroupBy,
                                    DocumentId = wf.DocumentId,
                                    RefBy = new List<WorkflowProcessItem> { wfdeputy },
                                    Submitted=null
                                };

                                _context.WorkflowProcessItems.Add(userFromDeputy);
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