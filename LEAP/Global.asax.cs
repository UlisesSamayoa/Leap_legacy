using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LEAP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
             
            
        }
        
        //public class Startup
        //{
        //    public void Configuration(IAppBuilder app)
        //    {
        //        app.UseCookieAuthentication(new CookieAuthenticationOptions
        //        {
        //            AuthenticationType = "ApplicationCookie",
        //            LoginPath = new PathString("/Account/Login"),
        //            ExpireTimeSpan = TimeSpan.FromMinutes(30),
        //            SlidingExpiration = true
        //        });
        //    }
        //}
        //protected void Application_AuthenticateRequest(object sender, EventArgs e)
        //{
        //    if (HttpContext.Current.User != null)
        //    {
        //        if (HttpContext.Current.User.Identity.IsAuthenticated)
        //        {
        //            // Obtener la identidad del usuario actual
        //            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

        //            // Agregar los claims necesarios
        //            identity.AddClaim(new Claim(ClaimTypes.Name, "username"));
        //            // Agregar otros claims según sea necesario
        //        }
        //    }
        //}


    }
}
