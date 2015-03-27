using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.AuditApp.Models
{
    public class ActionModel
    {
        public List<Action> Actions = new List<Action>();
        public ActionModel(int projectId)
        {
            var context = new AuditConnection();
            Actions = context.Actions
                             .Where(a => a.ProjectId == projectId && a.Closed == false)
                             .ToList();
        }
    }
}