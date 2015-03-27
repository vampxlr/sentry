using Breeze.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Freelance.AuditApp;
using Newtonsoft.Json.Linq;
namespace Freelance.AuditApp.Controllers
{   [BreezeController]
    public class BreezeController : ApiController
    {
    readonly BreezeRepository _repository = new BreezeRepository();

        [HttpGet]
        public string Metadata()
        {
            return _repository.Metadata;
        }

        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            return _repository.SaveChanges(saveBundle);
        }

        [HttpGet]
        public IQueryable<User> Users()
        {
            return _repository.Users;
        }

        [HttpGet]
        public IQueryable<Project> Projects()
        {
            return _repository.Projects;
        }

        [HttpGet]
        public IQueryable<UserProject> UserProjects()
        {
            return _repository.UserProjects;
        }

        [HttpGet]
        public IQueryable<AspectItem> AspectItems()
        {
            return _repository.AspectItems;
        }  

        [HttpGet]
        public IQueryable<ProjectAspect> ProjectAspects()
        {
            return _repository.ProjectAspects;
        }
    
        [HttpGet]
        public IQueryable<Action> Actions()
        {
            return _repository.Actions;
        }

        [HttpGet]
        public IQueryable<Result> Results()
        {
            return _repository.Results;
        }
    }
}
