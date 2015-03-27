using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.AuditApp.Models
{
    public class AuditModel
    {
        public List<ProjectAspect> ProjectAspects { get; set; }
    }

    public class ProjectAspect
    {
        public int AspectId { get; set; }
        public string ObservationalComments { get; set; }
        public string Satisfactory { get; set; }
        public string WeatherObservations { get; set; }
        public string DateRecorded { get; set; }
        public int ProjectId { get; set; }
        public string Auditee { get; set; }
    }


}