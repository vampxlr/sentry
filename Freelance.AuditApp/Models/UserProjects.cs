using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Web;

namespace Freelance.AuditApp.Models
{
    public class UserProjects
    {
        public IEnumerable<Project> Projects = new List<Project>();
        public List<Project> ExistingUserProjects = new List<Project>();
        public int UserId { get; set; }

        public UserProjects(int userId)
        {
            UserId = userId;
            var context = new AuditConnection();
            var existingProjectIds = context.UserProjects.Where(up => up.UserId == userId).Select(p => p.ProjectId);
            foreach (var id in existingProjectIds)
            {
                var project = context.Projects.Where(p => p.ProjectId == id).First();
                ExistingUserProjects.Add(project);
            }

            var proj = context.Projects.ToList();
            Projects = proj.Except(ExistingUserProjects);

        }
    }
}