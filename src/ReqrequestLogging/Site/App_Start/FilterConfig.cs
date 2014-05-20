using System.Web.Mvc;
using Site.Filters;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace Site
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            
            filters.Add(new HandleErrorAttribute());
        }
    }
}
