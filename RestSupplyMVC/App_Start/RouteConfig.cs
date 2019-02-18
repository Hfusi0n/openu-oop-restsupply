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
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Navigation", action = "Index", id = UrlParameter.Optional }
            );

            // create routes
            routes.MapRoute("CreateIngredients",
                "{controller}/{action}",
                new { controller = "Ingredients", action = "Create" });
            routes.MapRoute("CreateMenuItems",
                "menuItems/{action}",
                new { controller = "MenuItems", action = "Create" });
            routes.MapRoute("CreateMenuIngredients",
                "{controller}/{action}",
                new { controller = "MenuIngredients", action = "Create" });
            routes.MapRoute("CreateSuppliers",
                "{controller}/{action}",
                new { controller = "Suppliers", action = "Create" });
            routes.MapRoute("CreateCustomerOrder",
                "{controller}/{action}",
                new { controller = "CustomerOrder", action = "Create" });

            // index (GET) routes
            routes.MapRoute("GetKitchens",
                "{controller}/{action}",
                new { controller = "Kitchens", action = "Index" });
            routes.MapRoute("GetSuppliers",
                "{controller}/{action}",
                new { controller = "Suppliers", action = "Index" });
            routes.MapRoute("GetSupplierOrders",
                "{controller}/{action}",
                new { controller = "SupplierOrder", action = "Index" });
            routes.MapRoute("GetMenuItems",
                "{controller}/{action}",
                new { controller = "MenuItems", action = "Index" });
            routes.MapRoute("GetMenuIngredients",
                "{controller}/{action}",
                new { controller = "MenuIngredients", action = "Index" });
            routes.MapRoute("GetIngredients",
                "{controller}/{action}",
                new { controller = "Ingredients", action = "Index" });
            routes.MapRoute("GetCustomerOrder",
                "{controller}/{action}",
                new { controller = "CustomerOrder", action = "Index" });



        }
    }
}
