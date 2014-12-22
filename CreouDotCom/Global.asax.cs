using Ninject;
using Ninject.Web.Common;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;

namespace CreouDotCom
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Contact", // Route name
                "contact", // URL with parameters
                new { controller = "Contact", action = "Contact" } // Parameter defaults
            );

            routes.MapRoute(
                "Cv", // Route name
                "cv", // URL with parameters
                new { controller = "Home", action = "Cv" } // Parameter defaults
            );

            routes.MapRoute(
                "Blog", // Route name
                "blog", // URL with parameters
                new { controller = "Blog", action="Index"} // Parameter defaults
            );

            routes.MapRoute(
                "BlogPost", // Route name
                "blog/{blogId}", // URL with parameters
                new { controller = "Blog", action = "BlogPost", blogId = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();

        //    RegisterGlobalFilters(GlobalFilters.Filters);
        //    RegisterRoutes(RouteTable.Routes);
        //}
    }
}