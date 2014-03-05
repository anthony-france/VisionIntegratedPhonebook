using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VisionIntegratedPhonebook
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DepartmentFacility",
                url: "Department/{department}/{facility}",
                defaults: new
                {
                    controller = "Department",
                    action = "Details",
                    facility = ""
                }
            );

            routes.MapRoute(
                name: "Department",
                url: "Department/{location}",
                defaults: new
                {
                    controller = "Department",
                    action = "Details"
                }
            );

            routes.MapRoute(
                name: "FacilityDepartment",
                url: "Facility/{location}/{department}",
                defaults: new
                {
                    controller = "Facility",
                    action = "Details",
                    department = ""
                }
            );

            routes.MapRoute(
                name: "Facility",
                url: "Facility/{location}",
                defaults: new
                {
                    controller = "Facility",
                    action = "Details",
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            ); 
        }
    }
}
