using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.AuditApp.Models.ViewModels
{
    public class AddNewAspectItemViewModel
    {
        public int Id { get; set; }
        public int projectId { get; set; }
        public int projectAspectId { get; set; }
        public string aspectItemName { get; set; }

    }
}