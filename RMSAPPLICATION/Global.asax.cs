using Autofac;
using Autofac.Integration.Mvc;
using RMSAPPLICATION.Modules;
using RMSCORE.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace RMSAPPLICATION
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Autofac Configuration
            var builder = new Autofac.ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EFModule());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
        protected void Session_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Session["LoggedInUser"] = new V_UserCandidate();
            HttpContext.Current.Session["ToasterMessages"] = new List<string>();
            //LoadSession();
        }
        protected void Session_End(object sender, EventArgs e)
        {
            HttpContext.Current.Session["LoggedInUser"] = null;
            HttpContext.Current.Session["ToasterMessages"] = null;
        }
        //protected void Application_Error()
        //{
        //    HttpContext httpContext = HttpContext.Current;
        //    httpContext.Response.Redirect("~/Home/Error");
        //}
    }
}
