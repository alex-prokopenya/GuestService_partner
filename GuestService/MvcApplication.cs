namespace GuestService
{
    using GuestService.Code;
    using System;
    using System.Threading;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = base.Server.GetLastError();
            base.Response.Clear();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            WebSecurityInitializer.Initialize();
        }

        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (custom == "language")
            {
                return Thread.CurrentThread.CurrentUICulture.ToString();
            }
            return base.GetVaryByCustomString(context, custom);
        }
    }
}

