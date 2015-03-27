using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.AuditApp.Models.ViewModels
{
    public class AspectItemViewModel
    {
        public int AspectItemsID { get; set; }
        public Nullable<int> ProjectAspectID { get; set; }
        public string AspectItem { get; set; }
    }
}