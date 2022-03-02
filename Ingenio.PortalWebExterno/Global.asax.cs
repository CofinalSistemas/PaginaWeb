using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ingenio.PortalWebExterno
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MvcHandler.DisableMvcResponseHeader = true; //this line is to hide mvc header

            RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //    MvcHandler.DisableMvcResponseHeader = true;
            var app = sender as HttpApplication;
            if (app != null && app.Context != null)
            {
                try
                {
                    //         app.Context.Response.Headers.Remove( "Server" );
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            if (app != null && app.Context != null)
            {
                //app.Context.Response.Headers.Remove( "Server" );
                //app.Context.Response.Headers.Remove( "X-AspNet-Version" );
                //app.Context.Response.Headers.Remove( "X-AspNetMvc-Version" );
            }
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var errorr = Server.GetLastError();
            Trace.WriteLine($"{errorr.Source} - {errorr} - {DateTime.Now.ToShortDateString()}" );
            Trace.WriteLine( $"{ errorr.StackTrace}" );
            //HttpException httpException = Server.GetLastError() as HttpException;

            int error = 500;
            Response.Clear();
            Server.ClearError();
            Response.Redirect(String.Format("~/Error/?error={0}", error, errorr.Message));
        }

    }
}
