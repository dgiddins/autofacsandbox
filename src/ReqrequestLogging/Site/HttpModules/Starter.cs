using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Dependencies;
using Site.Models;

namespace Site.HttpModules
{
    public class Starter : DelegatingHandler
    {
        public Starter()
        {
            
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            
            IDependencyScope dependencyScope = request.GetDependencyScope();
            var timeLine = dependencyScope.GetService(typeof (RequestTimeline)) as RequestTimeline;
            timeLine.IdGuid = Guid.NewGuid();
            timeLine.SetBy = this.GetType().Name;
            return base.SendAsync(request, cancellationToken).ContinueWith(
                (task) =>
                {
                    //write log out here- async and don't wait for it to complete?
                    return task.Result;
                });
        }

    }
}