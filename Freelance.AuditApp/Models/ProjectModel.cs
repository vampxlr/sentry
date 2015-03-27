using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.ModelBinding;

namespace Freelance.AuditApp.Models
{
    public class ProjectModel
    {
        public List<AuditApp.Project> Projects = new List<Project>();
        public List<User> Users = new List<User>();

        public ProjectModel()
        {
            var context = new AuditConnection();
            Projects = context.Projects.ToList();
            Users = context.Users.ToList();
        }

        public ProjectModel(string username)
        {
            var context = new AuditConnection();

            var userId = context.Users.Where(u => u.Username == username).Select(ux => ux.UserId).FirstOrDefault();
            List<int> projectId = new List<int>();
            if (userId != null)
            {
                projectId = context.UserProjects.Where(up => up.UserId == userId).Select(p => p.ProjectId.Value).ToList();
            }

            Projects = context.Projects.Where(p => projectId.Contains(p.ProjectId)).ToList();
            Users = context.Users.ToList();
        }


    }

    public class ProjectModelUpdate
    { 
         public int ProjectId { get; set; }
         public String ProjectName { get; set; }
         public String RosterWork { get; set; }
         public String Location { get; set; }
         public String ProjectNumber { get; set; }
         public String RosterBreak { get; set; }
         public DateTime? StartDate { get; set; }
         public String Duration { get; set; }
    }
}