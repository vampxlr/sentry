using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.AuditApp.Models.ViewModels
{
    public class AddNewActionViewModel
    {     
        public int projectId { get; set; }
        public string description { get; set; }
        public string dateTime { get; set; }
        public string person { get; set; }
        public string priority { get; set; }
        public string user { get; set; }
    }
}