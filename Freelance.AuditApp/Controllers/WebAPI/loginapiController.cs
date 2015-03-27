using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Freelance.AuditApp.Models.ViewModels;
using System.Web.Mvc;
using Freelance.AuditApp.Models;
using System.Runtime.Serialization;
using System.Net.Mail;
using SpreadsheetLight;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Net.Http.Headers;

namespace Freelance.AuditApp.Controllers
{
    //[Authorize]  
    
    public class loginapiController : ApiController
    {

        [System.Web.Http.HttpGet]
        public IEnumerable<ProjectViewModel> unAssignedProjectsByID(int id)
        {
            UserProjects UserProjects = new UserProjects(id);
            IEnumerable< Project> Projects = UserProjects.Projects;
            IEnumerable<ProjectViewModel> UProjects = from U in Projects
                                                      select new ProjectViewModel { RosterWork = U.RosterWork, RosterBreak = U.RosterBreak, ProjectName = U.ProjectName, Id = U.ProjectId, ProjectNumber = U.ProjectNumber, Duration = U.Duration, Location = U.Location };
            return UProjects;
        }

        [System.Web.Http.HttpGet]
        public IEnumerable<ProjectViewModel> AssignedProjectsByID(int id)
        {
            UserProjects UserProjects = new UserProjects(id);
            IEnumerable<Project> Projects = UserProjects.ExistingUserProjects;
            IEnumerable<ProjectViewModel> UProjects = from U in Projects
                                                      select new ProjectViewModel { RosterWork = U.RosterWork, RosterBreak = U.RosterBreak, ProjectName = U.ProjectName, Id = U.ProjectId, ProjectNumber = U.ProjectNumber, Duration = U.Duration, Location = U.Location };
            return UProjects;
        }

        
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public IEnumerable<UserViewModel> UserDetails()
        {
             var context = new AuditConnection();

             IEnumerable<User> Users = context.Users.ToArray();
             IEnumerable<UserViewModel> UModel = from U in Users
                                                 select new UserViewModel
                                                 {
                                                     UserId = U.UserId,
                                                     UserName = U.Username,
                                                     Role = U.UserRole,
                                                     UserProjects = from z in U.UserProjects
                                                                    select new UserProjectViewModel { UserId = z.UserId, ProjectId = z.ProjectId, UserProjectId = z.UserProjectId }

                                                 };
            
             return UModel;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public IEnumerable<ProjectViewModel> ProjectListDetails()
        {
            var context = new AuditConnection();

            IEnumerable<Project> Users = context.Projects.ToList();
            IEnumerable<ProjectViewModel> UModel = from U in Users
                                                   select new ProjectViewModel {RosterWork=U.RosterWork,RosterBreak= U.RosterBreak, ProjectName = U.ProjectName, Id= U.ProjectId , ProjectNumber = U.ProjectNumber , Duration = U.Duration , Location = U.Location };

            return UModel;
        }
  
        [System.Web.Http.HttpPost]
        public bool postpeople(User User)
        {
            bool added = false;
            var service = new AuditService();
            added= service.AddUser(User.Username, User.UserPassword, User.UserRole);

            return added;
          
           
        }


            
          [System.Web.Http.HttpDelete]
        public bool removeproject(int id)
            {
                var service = new AuditService();
                service.RemoveProject(id);
                return true;
           
            }
          [System.Web.Http.HttpDelete]
          //public bool removeuser(int id)
          //{
          //    var service = new AuditService();
          //    service.RemoveUser(id);
          //    return true;

          //}

          [System.Web.Http.HttpPost]
          public bool AddUserToProject(UserToProjectViewModel UserToProject)
          {
              bool success=false;
              var service = new AuditService();
              success = service.AddUserToProject(UserToProject.ProjectId, UserToProject.UserId);
              return success;

          }

        [System.Web.Http.HttpPost]
          public bool RemoveuserFromProject(UserToProjectViewModel UserToProject)
          {
              bool success = false;
              var service = new AuditService();
              success = service.RemoveUserFromProject(UserToProject.ProjectId, UserToProject.UserId);
              return success;
          }

        [System.Web.Http.HttpPost]
        public bool postproject(AddProjectViewModel project)
        {
          
            bool added = false;
            var service = new AuditService();
            added = service.AddProject( project.ProjectName, 
                project.ProjectNumber, 
                project.Location, 
                project.RosterWork, 
                project.RosterBreak, 
                project.Duration, 
                project.StartDate);

            return added;


        }


        [System.Web.Http.HttpGet]
        public IEnumerable<ProjectAspectViewModel> getAspectByProjectId(int id)
        {
            var service = new AuditService();
            Project project = service.GetProject(id);

            try
            {
                IEnumerable<ProjectAspectViewModel> projectAspect = from U in project.ProjectAspects
                                                                    select new ProjectAspectViewModel
                                                                    {
                                                                        AspectName = U.Aspect,
                                                                        ProjectAspectsID = U.ProjectAspectsID,
                                                                        ProjectID = U.ProjectID,
                                                                        AspectItems = from x in U.AspectItems
                                                                                      select new AspectItemViewModel
                                                                                      {
                                                                                          AspectItem = x.AspectItem1,
                                                                                          AspectItemsID = x.AspectItemsID,
                                                                                          ProjectAspectID = x.ProjectAspectID
                                                                                      }
                                                                    };
                return projectAspect;
            }
            catch (Exception e )
            {
                
                throw e;
            }

           // IEnumerable<ProjectAspectViewModel> sprojectAspect;

         
        }

        [System.Web.Http.HttpGet]
        public AspectItem getAspectItemByAspectId(int id)
        {
            AuditConnection db = new AuditConnection();
            AspectItem aspectItem = db.AspectItems.SingleOrDefault(t => t.AspectItemsID == id);
            
            return aspectItem;
        }


        [System.Web.Http.HttpPost]
        public int AddNewAspect(AddNewAspectViewModel Aspect) {
            int success ;
            var service = new AuditService();
             success = service.AddNewProjectAspect(Aspect.projectId, Aspect.AspectName);
             return success;
        
        }


        
            [System.Web.Http.HttpPost]
        public bool AddNewAspectItem(AddNewAspectItemViewModel AspectItem)
        {
            var service = new AuditService();
            service.AddNewProjectAspectItem(AspectItem.projectId, AspectItem.projectAspectId, AspectItem.aspectItemName);
            return true;
        
        }

            [System.Web.Http.HttpGet]
            public Project getProjectById(int id)
            {
                AuditConnection db = new AuditConnection();
                Project project = db.Projects.SingleOrDefault(t=>t.ProjectId==id);

                return project;
            }

            [System.Web.Http.HttpPost]
            public void EditProjectById(ProjectViewModel project)
            {
                var service = new AuditService();
                service.UpdateProject(project.Id,project.ProjectName,project.ProjectNumber,project.Location,project.RosterWork,project.RosterBreak,project.Duration,project.StartDate);

            }
             [System.Web.Http.HttpPost]
            public bool SaveAudit(AuditModel model)
            {
               
                var coverted = model;
                var service = new AuditService();
                service.SaveAudit(coverted);
                return true;
            }

             [System.Web.Http.HttpPost]
             public bool AddNewAction(AddNewActionViewModel model)
             {
                 var service = new AuditService();

                 service.AddAction(model.projectId, model.description, model.dateTime, model.person, model.priority, model.user);
                 return true;
             }
             [System.Web.Http.HttpPost]
             public bool CloseUserAction(CloseUserActionViewModel model)
             {
                 var service = new AuditService();
                 service.CloseAction(model.actionid, model.description, model.dateClosed);
                 return true;
             }

     

             [System.Web.Http.HttpGet]
             public IEnumerable<Action> GetCloseUserActions(int id)
             {
                 

                 var context = new AuditConnection();
                 IEnumerable<Action> Actions = context.Actions
                                  .Where(a => a.ProjectId == id && a.Closed == false)
                                  .ToList();

                 return Actions;
             }
                [System.Web.Http.HttpPost]
             public IEnumerable<GetAllActionsByDateViewModel> QueryActions(QueryActionsViewModel model)
             {
                 var context = new AuditConnection();
                 DateTime start = DateTime.ParseExact(model.startDate, "dd/mm/yyyy", null);
                 DateTime end = DateTime.ParseExact(model.endDate, "dd/mm/yyyy", null);

                  IEnumerable<Action> Actions= context.Actions.Where(a => a.ClosedDate >= start && a.ClosedDate <= end).ToList();


                  IEnumerable<Project> Projects = context.Projects.ToList();
                  IEnumerable<ProjectViewModel> ProjectsViewModel = from U in Projects
                                                                    select new ProjectViewModel { RosterWork = U.RosterWork, RosterBreak = U.RosterBreak, ProjectName = U.ProjectName, Id = U.ProjectId, ProjectNumber = U.ProjectNumber, Duration = U.Duration, Location = U.Location };

                  var results = from Ac in Actions
                                join P in ProjectsViewModel on Ac.ProjectId equals P.Id

                                select new { Ac, P };

                  IEnumerable<GetAllActionsByDateViewModel> smodel = from r in results
                                                                     select new GetAllActionsByDateViewModel
                                                                     {
                                                                        ActionDescription = r.Ac.ActionDescription,
                                                                        ActionId = r.Ac.ActionId,
                                                                        Closed = r.Ac.Closed,
                                                                        ClosedDate=r.Ac.ClosedDate,
                                                                        ClosedFile=r.Ac.ClosedFile,
                                                                        ClosedReason=r.Ac.ClosedReason,
                                                                        CreatedBy=r.Ac.CreatedBy,
                                                                        CreatedDate=r.Ac.CreatedDate,
                                                                        DueDate=r.Ac.DueDate,
                                                                        Priority=r.Ac.Priority,
                                                                        RaisedBy=r.Ac.RaisedBy,
                                                                        ProjectNumber=r.P.ProjectNumber,
                                                                        ProjectName=r.P.ProjectName
                                                                     };



                 //--------------------------------------------

                  return smodel;
                
             }

                  [System.Web.Http.HttpGet]
             public ActionReportViewModel ActionReport()
             {
                 var context = new AuditConnection();
                 IEnumerable<Action> Actions = context.Actions.ToList();
                 ActionReportViewModel model = new ActionReportViewModel();
                 model.TotalActionsIdentified = Actions.Count();
                 model.TotalClosedActions = Actions.Count(a => a.Closed == true);
                 model.TotalProjects = context.Projects.Count();
                 model.TotalActionsPerProject = context.Actions.GroupBy(p => p.ProjectId).Count();
                 return model;   
                
             }

                  [System.Web.Http.HttpGet]
                  public IEnumerable<UserViewModel> GetUsersByProjectId(int id)
                  {

                      IEnumerable<User> users = new List<User>();
                        List<User> ExistingUserProjects = new List<User>();
                        int ProjectId;

                        ProjectId = id;
                      var context = new AuditConnection();
                      var useridsbyProjectid = context.UserProjects.Where(up => up.ProjectId == ProjectId).Select(p => p.UserId);
                      foreach (var uId in useridsbyProjectid)
                      {
                          var project = context.Users.Where(p => p.UserId == uId).First();
                          ExistingUserProjects.Add(project);
                      }

                      var user = context.Users.ToList();
                      users = user.Except(ExistingUserProjects);
                      IEnumerable<UserViewModel> UModel = from U in ExistingUserProjects
                                                          select new UserViewModel
                                                          {
                                                              UserId = U.UserId,
                                                              UserName = U.Username,
                                                              Role = U.UserRole,
                                                              UserProjects = from z in U.UserProjects
                                                                             select new UserProjectViewModel { UserId = z.UserId, ProjectId = z.ProjectId, UserProjectId = z.UserProjectId }

                                                          };
                      return UModel;


                  }

                  [System.Web.Http.HttpPost]
                  public bool SendEmail(SendEmailViewModel model)
                  {
                      SendEmail(model.EmailTo,model.EmailFrom,model.Subject,model.Body);
                      return true;

                  }
                  public static string SendEmail(string EmailtoList,string EmailFrom,string subject = "Not Defined", string body = "Not Defined")
                  {

                      string mailFrom = "sentry.emailer@gmail.com";


                      string EmailList = EmailtoList;

                      SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                      smtpClient.Credentials = new System.Net.NetworkCredential()
                      {
                          UserName = "sentry.emailer@gmail.com",
                          Password = "sentry007"
                      };
                      smtpClient.EnableSsl = true;


                      string[] EmailAddresses = EmailList.Split(',');
                      foreach (string EmailAddress in EmailAddresses)
                      {
                          MailMessage mailMessage = new MailMessage(mailFrom, EmailAddress, subject, body);


                          smtpClient.Send(mailMessage);
                      }


                      return "Mail Sent";
                  }

                  [System.Web.Http.HttpGet]
                  public IEnumerable<Result> GetAllResults()
                  {
                      AuditConnection context = new AuditConnection();
                      IEnumerable<Result> GetAllResults = context.Results.ToList();
                      return GetAllResults;
                  }
                  [System.Web.Http.HttpPost]
                  public IEnumerable<GetAllResultsByDateViewModel> GetAllResultsByDate(QueryResultsViewModel model)
                  {
                      DateTime start = DateTime.ParseExact(model.startDate, "dd/mm/yyyy", null);
                      DateTime end = DateTime.ParseExact(model.endDate, "dd/mm/yyyy", null);

                      AuditConnection context = new AuditConnection();
                      IEnumerable<Result> GetAllResults = context.Results.Where(r=>r.DateRecorded>=start && r.DateRecorded<=end).ToList();

                      IEnumerable<Project> Projects = context.Projects.ToList();
                      IEnumerable<ProjectViewModel> ProjectsViewModel = from U in Projects
                                                             select new ProjectViewModel { RosterWork = U.RosterWork, RosterBreak = U.RosterBreak, ProjectName = U.ProjectName, Id = U.ProjectId, ProjectNumber = U.ProjectNumber, Duration = U.Duration, Location = U.Location };


                      IEnumerable<AspectItem> AspectItems = context.AspectItems.ToList();
                      
                      var results = from R in GetAllResults
                                    join P in ProjectsViewModel on R.ProjectId equals P.Id
                                    join A in AspectItems on R.AspectItem equals A.AspectItemsID
                                    select new { R, P.ProjectName,A.AspectItem1 };

                      IEnumerable<GetAllResultsByDateViewModel> smodel = from r in results
                                                                        select new GetAllResultsByDateViewModel { 
                                                                            AspectItemName = r.AspectItem1,
                                                                            ProjectName = r.ProjectName,
                                                                            Auditees = r.R.Auditees,
                                                                            AspectItem = r.R.AspectItem, 
                                                                            ConductedBy = r.R.ConductedBy,
                                                                            DateRecorded = r.R.DateRecorded,
                                                                            ObservationalComment = r.R.ObservationalComment, 
                                                                            Satisfactory = r.R.Satisfactory, 
                                                                            AuditGuid = r.R.AuditGuid, 
                                                                            ProjectId = r.R.ProjectId, 
                                                                            WeatherObservations = r.R.WeatherObservations,
                                                                            ResultID = r.R.ResultID };

                      return smodel;
                  }
                     [System.Web.Http.HttpGet]
                  public IEnumerable<GetAllResultsByDateViewModel> GetAllResultsByUserId(int id)
                  {
                          

                      AuditConnection context = new AuditConnection();
                         User Users = context.Users.SingleOrDefault(r=>r.UserId==id);
                         IEnumerable<Result> GetAllResults = context.Results.Where(r => r.ConductedBy == Users.Username).ToList();

                      IEnumerable<Project> Projects = context.Projects.ToList();
                      IEnumerable<ProjectViewModel> ProjectsViewModel = from U in Projects
                                                                        select new ProjectViewModel { RosterWork = U.RosterWork, RosterBreak = U.RosterBreak, ProjectName = U.ProjectName, Id = U.ProjectId, ProjectNumber = U.ProjectNumber, Duration = U.Duration, Location = U.Location };


                      IEnumerable<AspectItem> AspectItems = context.AspectItems.ToList();

                      var results = from R in GetAllResults
                                    join P in ProjectsViewModel on R.ProjectId equals P.Id
                                    join A in AspectItems on R.AspectItem equals A.AspectItemsID
                                    select new { R, P.ProjectName, A.AspectItem1 };

                      IEnumerable<GetAllResultsByDateViewModel> smodel = from r in results
                                                                         select new GetAllResultsByDateViewModel
                                                                         {
                                                                             AspectItemName = r.AspectItem1,
                                                                             ProjectName = r.ProjectName,
                                                                             Auditees = r.R.Auditees,
                                                                             AspectItem = r.R.AspectItem,
                                                                             ConductedBy = r.R.ConductedBy,
                                                                             DateRecorded = r.R.DateRecorded,
                                                                             ObservationalComment = r.R.ObservationalComment,
                                                                             Satisfactory = r.R.Satisfactory,
                                                                             AuditGuid = r.R.AuditGuid,
                                                                             ProjectId = r.R.ProjectId,
                                                                             WeatherObservations = r.R.WeatherObservations,
                                                                             ResultID = r.R.ResultID
                                                                         };

                      return smodel;
                  }

                  public string ExportToExcelActionReport(QueryActionsViewModel model)
                  {
                      var context = new AuditConnection();
                      DateTime start = DateTime.ParseExact(model.startDate, "dd/mm/yyyy", null);
                      DateTime end = DateTime.ParseExact(model.endDate, "dd/mm/yyyy", null);

                      IEnumerable<Action> Actions = context.Actions.Where(a => a.ClosedDate >= start && a.ClosedDate <= end).ToList();


                      //-----------------------------------------

                      SLDocument sl = new SLDocument();
                      var count = 1;

                      sl.SetCellValue(count, 1, "ActionDescription");
                      sl.SetCellValue(count, 2, "ProjectId");
                      sl.SetCellValue(count, 3, "RaisedBy");
                      sl.SetCellValue(count, 4, "CreatedBy");
                      sl.SetCellValue(count, 5, "ActionId");
                      sl.SetCellValue(count, 6, "Closed");
                      sl.SetCellValue(count, 7, "ClosedDate");

                      sl.SetCellValue(count, 8, "ClosedReason");
                      sl.SetCellValue(count, 9, "CreatedDate");
                      sl.SetCellValue(count, 10, "DueDate");
                      sl.SetCellValue(count, 11, "Priority");

                      foreach (var item in Actions)
                      {
                          count++;
                          sl.SetCellValue(count, 1, item.ActionDescription);
                          sl.SetCellValue(count, 2, item.ProjectId.ToString());
                          sl.SetCellValue(count, 3, item.RaisedBy.ToString());
                          sl.SetCellValue(count, 4, item.CreatedBy.ToString()); 
                          sl.SetCellValue(count, 5, item.ActionId);
                          sl.SetCellValue(count, 6, item.Closed.ToString());
                          sl.SetCellValue(count, 7, item.ClosedDate.ToString());

                          sl.SetCellValue(count, 8, item.ClosedReason.ToString());
                          sl.SetCellValue(count, 9, item.CreatedDate.ToString());
                          sl.SetCellValue(count, 10, item.DueDate.ToString());
                          sl.SetCellValue(count, 11, item.Priority.ToString());



                      }

                    
                      sl.SaveAs("d://ActionReport.xlsx");

                      string fileName = "ActionReport";
                      return fileName;


                  }
                                   
    }
}
