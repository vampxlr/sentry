using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Freelance.AuditApp.Models.ViewModels
{
  
    public partial class ProjectViewModel
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectNumber { get; set; }
        public string RosterBreak { get; set; }
        public string RosterWork { get; set; }
        public string Location { get; set; }
        public string Duration { get; set; }
        public DateTime? StartDate { get; set; }
    }   
}