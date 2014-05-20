using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using Microsoft.Ajax.Utilities;
using Site.Models;

namespace Site.Filters
{
    public class Timelinstarter : IAutofacActionFilter
    {
        private readonly RequestTimeline _timeline;

        public Timelinstarter(RequestTimeline timeline)
        {
            _timeline = timeline;
        }

        public void OnActionExecuting(HttpActionContext actionContext)
        {
            if (_timeline.IdGuid == Guid.Empty)
            {
                _timeline.IdGuid = Guid.NewGuid();
                _timeline.SetBy = this.GetType().Name;
            } 
        }

        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            
        }
    }
}