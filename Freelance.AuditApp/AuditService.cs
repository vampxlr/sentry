using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using Freelance.AuditApp.Models;

namespace Freelance.AuditApp
{
    public class AuditService
    {
        private AuditApp.AuditConnection context = new AuditConnection();

        public bool AddUser(string username, string password, string userole)
        {
            context.Users.Add(new User()
            {
                UserPassword = password,
                Username = username,
                UserRole = userole
            });
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {

                var x = e;
                throw;
            }
           
            return true;
        }

        public bool AddUserToProject(int projectId, int userId)
        {
            context.UserProjects.Add(new UserProject()
            {
                ProjectId = projectId
                ,
                UserId = userId
            });

            context.SaveChanges();

            return true;
        }

        public bool RemoveUser(int userId)
        {
            var userProjects = context.UserProjects.Where(u => u.UserId == userId).ToList();
            var user = context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user != null)
            {
               foreach (var project in userProjects)
               {
                 context.UserProjects.Remove(project);
               }

                context.Users.Remove(user);
                context.SaveChanges();
            }

            return true;
        }

        public bool RemoveProject(int projectId)
        {
            var project = context.Projects.FirstOrDefault(p => p.ProjectId == projectId);
            
            if (project != null)
            {
                var userProjects = context.UserProjects.Where(up => up.ProjectId == project.ProjectId).ToList();

                foreach (var p in userProjects)
                {
                    context.UserProjects.Remove(p);
                }
                
                context.Projects.Remove(project);
                context.SaveChanges();
            }
            
            return true;
        }

        public bool RemoveUserFromProject(int projectId, int userId)
        {
            var projects = context.UserProjects.Where(p => p.ProjectId == projectId && p.UserId == userId).ToList();

            foreach (var p in projects)
            {
                context.UserProjects.Remove(p);
            }

            context.SaveChanges();
            return true;
        }

        public bool AddProject(string projectName, string projectNumber, string location, string rosterWork, string rosterBreak, string duration, string projectStartDate)
        {
            DateTime startDate = DateTime.ParseExact(projectStartDate, "dd/mm/yyyy", null);
            context.Projects.Add(new Project()
            {
                ProjectName = projectName
                , Location = location
                , RosterWork = rosterWork
                , RosterBreak = rosterBreak
                , StartDate = startDate
                , Duration = duration
                , ProjectNumber = projectNumber
            });

            context.SaveChanges();
            return true;
        }
        
        public bool UpdateProject(Int32 projectId, string projectName, string projectNumber, string location, string rosterWork, string rosterBreak, string duration, DateTime? projectStartDate)
        {
            var project = context.Projects.Where(p=> p.ProjectId == projectId).FirstOrDefault();
            project.ProjectName = projectName;
            project.ProjectNumber = projectNumber;
            project.Location = location;
            project.RosterWork = rosterWork;
            project.RosterBreak = rosterBreak;
            project.Duration = duration;
            project.StartDate = projectStartDate;

            context.SaveChanges();
            return true;
        }

        public bool AddAspectItem(int projectId, string question)
        {
            return false;
        }

        public bool UserAuth(string username, string password)
        {
            return context.Users.Any(u => u.Username == username && u.UserPassword == password);
        }

        public string DetermineUserRole(string username)
        {
            return context.Users.FirstOrDefault(u => u.Username == username).UserRole;
        }
        public int DetermineUserId(string username)
        {
            return context.Users.FirstOrDefault(u => u.Username == username).UserId;
        }

        public bool AddAction(int projectId, string description, string dateTime, string person, string priority, string recordedBy)
        {
            DateTime dateRecorded = DateTime.Now;
            DateTime dueDate = DateTime.ParseExact(dateTime, "dd/mm/yyyy", null);

            int userId = context.Users.FirstOrDefault(u => u.Username == recordedBy).UserId;
            context.Actions.Add(new Action()
            {
                ProjectId = projectId
                , Closed = false
                , Priority = priority
                , RaisedBy = person
                , CreatedBy = userId
                , CreatedDate = dateRecorded
                , DueDate =  dueDate
                , ActionDescription = description
            });

            context.SaveChanges();

            return true;
        }

        public void CloseAction(int actionId, string description, string dateClosed)
        {
            var action = context.Actions.FirstOrDefault(a => a.ActionId == actionId);
            DateTime closedDate = DateTime.ParseExact(dateClosed, "dd/mm/yyyy", null);
            if (action != null)
            {
                action.Closed = true;
                action.ClosedReason = description;
                action.ClosedDate = closedDate;
                context.SaveChanges();
            }
        }

        public List<Action> QueryActions(string startDate, string endDate)
        {
            DateTime start = DateTime.ParseExact(startDate, "dd/mm/yyyy", null);
            DateTime end = DateTime.ParseExact(endDate, "dd/mm/yyyy", null);

            return context.Actions.Where(a => a.ClosedDate >= start && a.ClosedDate <= end).ToList();
        }

        public Project GetProject(int projectId)
        {
            return context.Projects.FirstOrDefault(p => p.ProjectId == projectId);
        }

        public int AddNewProjectAspect(int projectId, string aspect)
        {
            int retVal = 0;
            var project = context.Projects.FirstOrDefault(p => p.ProjectId == projectId);
            if (project != null)
            {
                project.ProjectAspects.Add(new ProjectAspect()
                {
                    Aspect = aspect
                    , ProjectID = projectId
                });
                context.SaveChanges();
                retVal = context.ProjectAspects.OrderByDescending(o => o.ProjectAspectsID).FirstOrDefault().ProjectAspectsID;
            }

            return retVal;
        }

        public bool AddNewProjectAspectItem(int projectId, int projectAspectId, string aspect)
        {
            var project = context.Projects.FirstOrDefault(p => p.ProjectId == projectId);
            if (project != null)
            {
                var aspects = project.ProjectAspects.Where(pa => pa.ProjectAspectsID == projectAspectId);
                var aspectItems = aspects.FirstOrDefault();

                if (aspectItems != null)
                {
                    aspectItems.AspectItems.Add(new AspectItem()
                    {
                        AspectItem1 = aspect
                        , ProjectAspectID = projectAspectId
                    });
                }
              
                context.SaveChanges();
            }

            return true;
        }

        public bool DeleteProjectAspect(int projectAspectId)
        {
            var aspects = context.ProjectAspects.Where(pa => pa.ProjectAspectsID == projectAspectId);
            foreach (var asp in aspects)
            {
                var aspectItems = context.AspectItems.Where(p => p.ProjectAspectID == projectAspectId);
                foreach (var item in aspectItems)
                {
                    context.AspectItems.Remove(item);
                }
       
                context.ProjectAspects.Remove(asp);
            }

            context.SaveChanges();
            return true;
        }

        public bool DeleteProjectAspectItem(int projectAspectItemId)
        {
            var aspects = context.AspectItems.Where(p => p.AspectItemsID == projectAspectItemId).ToList();

            foreach (var asp in aspects)
            {
                context.AspectItems.Remove(asp);
            }

            context.SaveChanges();

            return true;
        }

        public void SaveAudit(AuditModel model)
        {
            Guid g = Guid.NewGuid();
        
            foreach (var m in model.ProjectAspects)
            {
                context.Results.Add(new Result()
                {
                    AuditGuid = g.ToString(),
                    ProjectId = m.ProjectId
                    , AspectItem = m.AspectId
                    , ObservationalComment = m.ObservationalComments
                    , Satisfactory = m.Satisfactory
                    , WeatherObservations = m.WeatherObservations
                    , Auditees = m.Auditee
                    , DateRecorded = DateTime.ParseExact(m.DateRecorded, "dd/mm/yyyy", null)
                    , ConductedBy = m.Auditee
                }); 
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Exception x = e;

                throw;
            }
          
        }
    }
}