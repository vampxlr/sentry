using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Freelance.AuditApp.Models;

namespace Freelance.AuditApp.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public bool AddNewUser(string username, string password, string role)
        {
            var service = new AuditService();
            service.AddUser(username, password, role);

            return true;
        }

        public bool AddNewProject(string projectName, string projectNumber, string location, string rosterWork, string rosterBreak, string projectStartDate, string duration)
        {
            var service = new AuditService();
            service.AddProject(projectName, projectNumber, location, rosterWork, rosterBreak, duration, projectStartDate);
            return true;
        }

        public bool AddNewAction(int projectId, string description, string dateTime, string person, string priority)
        {
            var service = new AuditService();
            string user = Session["Username"].ToString();
            service.AddAction(projectId, description, dateTime, person, priority, user);
            return true;
        }

        public bool SaveAudit(string model)
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            var coverted = serial.Deserialize<AuditModel>(model);
            var service = new AuditService();
            service.SaveAudit(coverted);
            return true;
        }

        public bool UploadFiles(HttpPostedFileBase file)
        {
            var f = file;
            var files = Request.Files;
            int count = Request.Files.Count;
            return true;
        }
    }
}
