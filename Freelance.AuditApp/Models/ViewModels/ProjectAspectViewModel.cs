using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.AuditApp.Models.ViewModels
{
    public class ProjectAspectViewModel
    {
        public int ProjectAspectsID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public string AspectName { get; set; }

        public IEnumerable<AspectItemViewModel> AspectItems { get; set; }
    }
}