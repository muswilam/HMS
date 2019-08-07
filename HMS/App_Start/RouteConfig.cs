using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "FEAccommodations",
                url: "Accommodations",
                defaults: new { area ="", controller = "Accommodations", action = "Index"},
                namespaces: new[] {"HMS.Controllers"}  // need namespace to compare between area/dashboaed/controllers and controllers
            );

            routes.MapRoute(
               name: "AccommodationPackageDetails",
               url: "accommodation-package/{accommodationPackageId}",
               defaults: new { area = "", controller = "Accommodations", action = "Details" },
               namespaces: new[] { "HMS.Controllers" } 
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
