using Freelance.AuditApp.Models.ViewModels;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Freelance.AuditApp.Controllers
{
    public class SPAController : Controller
    {
        //
        // GET: /SPA/
        [Authorize]
        public ActionResult Index()
        {
            if (Session["UserName"] == null || Session["UserRole"] == null || Session["UserId"]==null) { FormsAuthentication.SignOut(); return RedirectToAction("Login"); }
           

            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username,string password)
        {
            var service = new AuditService();
            if (service.UserAuth(username, password))
            {
                Session["UserName"] = username;
                FormsAuthentication.SetAuthCookie(username, true);
                Session["UserRole"] = service.DetermineUserRole(username);
                Session["UserId"] = service.DetermineUserId(username);
                var test = Session["UserId"].ToString();
                return RedirectPermanent("~/"); 
            }
            return View();
        }
        
        [HttpGet]
        
        public ActionResult LogOut()
        {
            Session["UserName"] = null;
            Session["UserRole"] = null; 
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        [HttpPost]
        public void ExportToExcelActionReport(QueryActionsViewModel model)
        {
            var context = new AuditConnection();
            DateTime start = DateTime.ParseExact(model.startDate, "dd/mm/yyyy", null);
            DateTime end = DateTime.ParseExact(model.endDate, "dd/mm/yyyy", null);

            IEnumerable<Action> Actions = context.Actions.Where(a => a.ClosedDate >= start && a.ClosedDate <= end).ToList();

            IEnumerable<Project> Projects = context.Projects.ToList();
            IEnumerable<ProjectViewModel> ProjectsViewModel = from U in Projects
                                                              select new ProjectViewModel { RosterWork = U.RosterWork, RosterBreak = U.RosterBreak, ProjectName = U.ProjectName, Id = U.ProjectId, ProjectNumber = U.ProjectNumber, Duration = U.Duration, Location = U.Location };

            var results = from Ac in Actions
                          join P in ProjectsViewModel on Ac.ProjectId equals P.Id

                          select new { Ac, P };

            IEnumerable<GetAllActionsByDateViewModel> smodel = from r in results
                                                               select new GetAllActionsByDateViewModel
                                                               {
                                                                   ActionDescription = r.Ac.ActionDescription,
                                                                   ActionId = r.Ac.ActionId,
                                                                   Closed = r.Ac.Closed,
                                                                   ClosedDate = r.Ac.ClosedDate,
                                                                   ClosedFile = r.Ac.ClosedFile,
                                                                   ClosedReason = r.Ac.ClosedReason,
                                                                   CreatedBy = r.Ac.CreatedBy,
                                                                   CreatedDate = r.Ac.CreatedDate,
                                                                   DueDate = r.Ac.DueDate,
                                                                   Priority = r.Ac.Priority,
                                                                   RaisedBy = r.Ac.RaisedBy,
                                                                   ProjectNumber = r.P.ProjectNumber,
                                                                   ProjectName = r.P.ProjectName
                                                               };



            //-----------------------------------------

            SLDocument sl = new SLDocument();
            var count = 1;
            sl.SetCellValue(count, 1, "ProjectName");
            sl.SetCellValue(count, 2, "ProjectNumber");
            sl.SetCellValue(count, 3, "ActionDescription");
            sl.SetCellValue(count, 4, "ProjectId");
            sl.SetCellValue(count, 5, "RaisedBy");
            sl.SetCellValue(count, 6, "CreatedBy");
            sl.SetCellValue(count, 7, "ActionId");
            sl.SetCellValue(count, 8, "Closed");
            sl.SetCellValue(count, 9, "ClosedDate");
            sl.SetCellValue(count, 10, "ClosedReason");
            sl.SetCellValue(count, 11, "CreatedDate");
            sl.SetCellValue(count, 12, "DueDate");
            sl.SetCellValue(count, 13, "Priority");

            foreach (var item in smodel)
            {
                count++;
                sl.SetCellValue(count, 1, item.ProjectName);
                sl.SetCellValue(count, 2, item.ProjectNumber);
                sl.SetCellValue(count, 3, item.ActionDescription);
                sl.SetCellValue(count, 4, item.ProjectId.ToString());
                sl.SetCellValue(count, 5, item.RaisedBy.ToString());
                sl.SetCellValue(count, 6, item.CreatedBy.ToString());
                sl.SetCellValue(count, 7, item.ActionId);
                sl.SetCellValue(count, 8, item.Closed.ToString());
                sl.SetCellValue(count, 9, item.ClosedDate.ToString());
                sl.SetCellValue(count, 10, item.ClosedReason.ToString());
                sl.SetCellValue(count, 11, item.CreatedDate.ToString());
                sl.SetCellValue(count, 12, item.DueDate.ToString());
                sl.SetCellValue(count, 13, item.Priority.ToString());



            }




            string writepath = Path.Combine(Server.MapPath("~/Files/Excel/"), "ActionReport" + Session["UserId"] + ".xlsx");

            sl.SaveAs(writepath);

       

        }

        [HttpGet]
        public FileResult ExportToExcelActionReport(string filename)
        {
            string writepath = Path.Combine(Server.MapPath("~/Files/Excel/"), "ActionReport" + Session["UserId"] + ".xlsx");


            return File(writepath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");


        }


        [HttpPost]
        public void ExportToExcelInspectionReport(QueryActionsViewModel model)
        {
        
            DateTime start = DateTime.ParseExact(model.startDate, "dd/mm/yyyy", null);
            DateTime end = DateTime.ParseExact(model.endDate, "dd/mm/yyyy", null);

            AuditConnection context = new AuditConnection();
            IEnumerable<Result> GetAllResults = context.Results.Where(r => r.DateRecorded >= start && r.DateRecorded <= end).ToList();

            IEnumerable<Project> Projects = context.Projects.ToList();
            IEnumerable<ProjectViewModel> ProjectsViewModel = from U in Projects
                                                              select new ProjectViewModel { RosterWork = U.RosterWork, RosterBreak = U.RosterBreak, ProjectName = U.ProjectName, Id = U.ProjectId, ProjectNumber = U.ProjectNumber, Duration = U.Duration, Location = U.Location };


            IEnumerable<AspectItem> AspectItems = context.AspectItems.ToList();

            var results = from R in GetAllResults
                          join P in ProjectsViewModel on R.ProjectId equals P.Id
                          join A in AspectItems on R.AspectItem equals A.AspectItemsID
                          select new { R, P.ProjectName, A.AspectItem1 };

            IEnumerable<GetAllResultsByDateViewModel> smodel = from r in results
                                                               select new GetAllResultsByDateViewModel
                                                               {
                                                                   AspectItemName = r.AspectItem1,
                                                                   ProjectName = r.ProjectName,
                                                                   Auditees = r.R.Auditees,
                                                                   AspectItem = r.R.AspectItem,
                                                                   ConductedBy = r.R.ConductedBy,
                                                                   DateRecorded = r.R.DateRecorded,
                                                                   ObservationalComment = r.R.ObservationalComment,
                                                                   Satisfactory = r.R.Satisfactory,
                                                                   AuditGuid = r.R.AuditGuid,
                                                                   ProjectId = r.R.ProjectId,
                                                                   WeatherObservations = r.R.WeatherObservations,
                                                                   ResultID = r.R.ResultID
                                                               };
            SLDocument sl = new SLDocument();
            var count = 1;

        
            sl.SetCellValue(count, 1, "ProjectName");
            sl.SetCellValue(count, 2, "AspectItemName");
            sl.SetCellValue(count, 3, "Auditees");
            sl.SetCellValue(count, 4, "ConductedBy");
            sl.SetCellValue(count, 5, "DateRecorded");
            sl.SetCellValue(count, 6, "ObservationalComment");
            sl.SetCellValue(count, 7, "Satisfactory");
            sl.SetCellValue(count, 8, "WeatherObservations");
    

            foreach (var item in smodel)
            {
                count++;

                sl.SetCellValue(count, 1, item.ProjectName.ToString());
                sl.SetCellValue(count, 2, item.AspectItemName.ToString());
                sl.SetCellValue(count, 3, item.Auditees.ToString());
                sl.SetCellValue(count, 4, item.ConductedBy.ToString());
                sl.SetCellValue(count, 5, item.DateRecorded.ToString());
                sl.SetCellValue(count, 6, item.ObservationalComment.ToString());
                sl.SetCellValue(count, 7, item.Satisfactory.ToString());
                sl.SetCellValue(count, 8, item.WeatherObservations.ToString());
            



            }




            string writepath = Path.Combine(Server.MapPath("~/Files/Excel/"), "InspectionReport" + Session["UserId"] + ".xlsx");

            sl.SaveAs(writepath);



        }

        [HttpGet]
        public FileResult ExportToExcelInspectionReport(string filename)
        {
            string writepath = Path.Combine(Server.MapPath("~/Files/Excel/"), "InspectionReport" + Session["UserId"] + ".xlsx");


            return File(writepath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");


        }



        public ActionResult Test()
        {
            return View();
        }
    }
}
