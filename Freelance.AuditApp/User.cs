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
    
    public partial class User
    {
        public User()
        {
            this.UserProjects = new HashSet<UserProject>();
            this.Projects = new HashSet<Project>();
        }
    
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; }
    
        public virtual ICollection<UserProject> UserProjects { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}