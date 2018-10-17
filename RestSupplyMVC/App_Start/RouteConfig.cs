using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestSupplyMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("MenuItems",
                "menuitems/{action}",
                new {controller = "MenuItems", action = "Create"});

            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Navigation", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute("Suppliers",
                "{controller}/{action}",
                new { controller = "Suppliers", action = "Index" });

        }
    }
}
