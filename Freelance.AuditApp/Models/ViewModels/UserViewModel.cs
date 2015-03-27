using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.AuditApp.Models.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public IEnumerable<UserProjectViewModel> UserProjects { get; set; }
        public IEnumerable<ProjectViewModel> Projects { get; set; }
    }
}