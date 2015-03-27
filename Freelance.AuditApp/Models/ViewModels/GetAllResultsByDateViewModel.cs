using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.AuditApp.Models.ViewModels
{
    public class GetAllResultsByDateViewModel
    {
        public int ResultID { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<int> AspectItem { get; set; }
        public string ObservationalComment { get; set; }
        public string Satisfactory { get; set; }
        public string ConductedBy { get; set; }
        public string Auditees { get; set; }
        public string WeatherObservations { get; set; }
        public string AuditGuid { get; set; }
        public Nullable<System.DateTime> DateRecorded { get; set; }
        public string ProjectName { get; set; }
        public string AspectItemName { get; set; }
    }
}