using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.AuditApp.Models.ViewModels
{
    public class AddNewAspectViewModel
    {

        public int Id { get; set; }
        public int projectId { get; set; }
        public string AspectName { get; set; }

        
    }
}