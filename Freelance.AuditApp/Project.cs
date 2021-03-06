//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Freelance.AuditApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Project
    {
        public Project()
        {
            this.ProjectAspects = new HashSet<ProjectAspect>();
            this.UserProjects = new HashSet<UserProject>();
        }
    
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string RosterWork { get; set; }
        public string Location { get; set; }
        public string ProjectNumber { get; set; }
        public string RosterBreak { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public string Duration { get; set; }
    
        public virtual ICollection<ProjectAspect> ProjectAspects { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<UserProject> UserProjects { get; set; }
    }
}
