using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Site.Filters;
using Site.HttpModules;
using Site.Models;

namespace Site
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule<NServiceBusModule>();
            builder.RegisterType<RequestTimeline>().InstancePerRequest();
            builder.RegisterType<SendOnlyBusLogger>();
            builder.Register(c => new Timelinstarter(c.Resolve<RequestTimeline>()))
                .AsWebApiActionFilterFor<ApiController>()
                .InstancePerRequest();

            builder.RegisterWebApiFilterProvider(config);

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);

            config.MessageHandlers.Add(new Starter());

            //GlobalConfiguration.Configuration.DependencyResolver = resolver;

            config.DependencyResolver = resolver;

            // Web API configuration and services
            

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
