using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.AuditApp.Models.ViewModels
{
    public class UserProjectViewModel
    {
        public int UserProjectId { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<int> UserId { get; set; }
    
    }
}