using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Breeze.WebApi;
using Newtonsoft.Json.Linq;
using Freelance.AuditApp.Models;
namespace Freelance.AuditApp
{
    public class BreezeRepository
    {
        private readonly Breeze.WebApi.EF.EFContextProvider<AuditConnection>
            _contextProvider = new Breeze.WebApi.EF.EFContextProvider<AuditConnection>();

        private AuditConnection Context { get { return _contextProvider.Context; } }

        public string Metadata
        {
            get { return _contextProvider.Metadata(); }
        }

        public SaveResult SaveChanges(JObject saveBundle)
        {
            return _contextProvider.SaveChanges(saveBundle);
        }

        public IQueryable<UserProject> UserProjects
        {
            get { return Context.UserProjects; }
        }

        public IQueryable<User> Users
        {
            get { return Context.Users; }
        }

        public IQueryable<AspectItem> AspectItems
        {
            get { return Context.AspectItems; }
        }

        public IQueryable<ProjectAspect> ProjectAspects
        {
            get { return Context.ProjectAspects; }
        }

        public IQueryable<Action> Actions
        {
            get { return Context.Actions; }
        }

        public IQueryable<Result> Results
        {
            get { return Context.Results; }
        }

        public IQueryable<Project> Projects
        {
            get { return Context.Projects; }
        }


    }
}