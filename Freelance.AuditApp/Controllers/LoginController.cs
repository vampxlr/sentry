using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Freelance.AuditApp.Models;

namespace Freelance.AuditApp.Controllers
{

    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }


        public JsonResult Authenticate(string username, string password)
        {
            var service = new AuditService();
            if (service.UserAuth(username, password))
            {
                Session["Username"] = username;
                return Json(new { authorized = true });
            }
            return Json(new { authorized = false });
        }

        public ActionResult Unauthorized()
        {
            return View("Unauthorized");
        }

        public ActionResult ProjectAspects(int projectId)
        {
            return View("ProjectAspects", new PartialProjectModel(projectId));
        }

        public ActionResult CloseActions(int projectId)
        {
            return View("Actions", new ActionModel(projectId));
        }

        public bool CloseUserAction(int actionid, string description, string dateClosed)
        {
            var service = new AuditService();
            service.CloseAction(actionid, description, dateClosed);
            return true;
        }

        public ActionResult QueryActions(string startDate, string endDate)
        {
            var service = new AuditService();
            var actions = service.QueryActions(startDate, endDate);

            return View("FilteredActions", actions);
        }

        public ActionResult EditAspects(int projectId)
        {
            var service = new AuditService();
            var aspects = service.GetProject(projectId);
            return View("EditAspects", aspects);
        }

        public ActionResult ViewAspects(int projectId)
        {
            var service = new AuditService();
            var aspects = service.GetProject(projectId);
            return View("ViewAspects", aspects);                                                      
        }

        public JsonResult AddNewProjectAspect(int projectId, string aspect)
        {
            var service = new AuditService();
            int val = service.AddNewProjectAspect(projectId, aspect);
            return Json(new { id = val });
        }

        public bool AddNewProjectAspectItem(int projectId, int projectAspectId, string aspect)
        {
            var service = new AuditService();
            service.AddNewProjectAspectItem(projectId, projectAspectId, aspect);
            return true;
        }

        public bool DeleteProjectAspect(int projectAspectId)
        {
            var service = new AuditService();
            service.DeleteProjectAspect(projectAspectId);
            return true;
        }

        public bool DeleteProjectAspectItem(int projectAspectItemId)
        {
            var service = new AuditService();
            service.DeleteProjectAspectItem(projectAspectItemId);
            return true;
        }

        public bool Removeuser(int userId)
        {
            var service = new AuditService();
            service.RemoveUser(userId);
            return true;
        }

        public bool RemoveProject(int projectId)
        {
            var service = new AuditService();
            service.RemoveProject(projectId);
            return true;
        }

        public ActionResult ExistingUserProjects(int userId)
        {
            return View("ExistingUserProjects", new UserProjects(userId));
        }

        public bool AddUserToProject(int projectId, int userId)
        {
            var service = new AuditService();
            service.AddUserToProject(projectId, userId);
            return true;
        }

        public ActionResult Authorized()
        {
            var service = new AuditService();

            if (Session["Username"] == null)
            {
                return View("Unauthorized");
            }

            string username = Session["Username"].ToString();

            if (String.IsNullOrWhiteSpace(username))
            {
                return View("Unauthorized");
            }

            string userRole = service.DetermineUserRole(username);

            if (userRole == "administrator")
            {
                return View("Administrator", new ProjectModel());
            }

            if (userRole == "analyst")
            {
                return View("ActionReport", new ActionReportModel());
            }

            return View("Audits", new ProjectModel(username));
        }

        public bool RemoveuserFromProject(int projectId, int userId)
        {
            var service = new AuditService();
            service.RemoveUserFromProject(projectId, userId);
            return true;
        }

        public ActionResult EditProject(int projectId)
        {
            var service = new AuditService();
            var objProj = service.GetProject(projectId);
            ProjectModelUpdate model = new ProjectModelUpdate();
            model.ProjectId = objProj.ProjectId;
            model.ProjectName = objProj.ProjectName;
            model.RosterWork = objProj.RosterWork;
            model.Location = objProj.Location;
            model.ProjectNumber = objProj.ProjectNumber;
            model.RosterBreak = objProj.RosterBreak;
            model.StartDate = objProj.StartDate;
            model.Duration = objProj.Duration;

            return View("PartialEditProject", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProject(ProjectModelUpdate model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // update
                    var service = new AuditService();
                    //service.UpdateProject(model.ProjectId, model.ProjectName, model.ProjectNumber, model.Location, model.RosterWork, model.RosterBreak, model.Duration, model.StartDate);
                    ViewBag.SuccessMessage = "Project saved successfully!";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return PartialView("PartialEditProject", model);
        }
    }


}
