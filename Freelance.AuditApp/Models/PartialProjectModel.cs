using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.AuditApp.Models
{
    public class PartialProjectModel
    {
        public List<AuditApp.Project> Projects = new List<Project>();

        public PartialProjectModel(int projectId)
        {
            var context = new AuditConnection();
            var pr = context.Projects.FirstOrDefault(p => p.ProjectId == projectId);
            if (pr != null)
            {
                Projects.Add(pr);
            }
        }
    }
}