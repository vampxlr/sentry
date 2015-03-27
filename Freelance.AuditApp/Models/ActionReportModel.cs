using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.AuditApp.Models
{
    public class ActionReportModel
    {
        public List<Action> Actions = new List<Action>();
        public int TotalActionsIdentified { get; set; }
        public int TotalClosedActions { get; set; }
        public int TotalInspectionsCompleted { get; set; }
        public int TotalProjects { get; set; }
        public int TotalActionsPerProject { get; set; }

        public ActionReportModel()
        {
            var context = new AuditConnection();
            Actions = context.Actions.ToList();

            TotalActionsIdentified = Actions.Count();
            TotalClosedActions = Actions.Count(a => a.Closed == true);
            TotalProjects = context.Projects.Count();
            TotalActionsPerProject = context.Actions.GroupBy(p => p.ProjectId).Count();


        }
    }
}