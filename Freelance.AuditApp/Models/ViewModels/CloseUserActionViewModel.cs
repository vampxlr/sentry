using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.AuditApp.Models.ViewModels
{
    public class CloseUserActionViewModel
    {
        public int actionid { get; set; }
        public string description { get; set; }
        public string dateClosed { get; set; }
    }
}