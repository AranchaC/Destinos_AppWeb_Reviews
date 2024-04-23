using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Destinos
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Contact",
            //    url: "Contact",
            //    defaults: new { controller = "Home", action = "Contact" }
            //);

            //routes.MapRoute(
            //    name: "Destinos",
            //    url: "Destinos",
            //    defaults: new { controller = "Destinos", action = "Destinos" }
            //);

            //routes.MapRoute(
            //    name: "Resenas",
            //    url: "Resenas",
            //    defaults: new { controller = "Resenas", action = "Resenas" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
