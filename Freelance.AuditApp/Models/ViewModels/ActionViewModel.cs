using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.AuditApp.Models.ViewModels
{
    public class ActionViewModel
    {
        public int ActionId { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<bool> Closed { get; set; }
        public string Priority { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public byte[] UploadedFile { get; set; }
        public string ActionDescription { get; set; }
        public string ClosedReason { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public byte[] ClosedFile { get; set; }
        public string RaisedBy { get; set; }
    }
}