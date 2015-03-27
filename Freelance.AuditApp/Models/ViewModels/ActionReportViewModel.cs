using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.AuditApp.Models.ViewModels
{
    public class ActionReportViewModel
    {
        public int TotalActionsIdentified { get; set; }
        public int TotalClosedActions { get; set; }
        public int TotalInspectionsCompleted { get; set; }
        public int TotalProjects { get; set; }
        public int TotalActionsPerProject { get; set; }
    }
}