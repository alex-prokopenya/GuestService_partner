﻿namespace GuestService
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            string name = "error";
            string url = "error/{code}";
            object defaults = new {
                controller = "error",
                action = "index",
                code = UrlParameter.Optional
            };
            routes.MapRoute(name, url, defaults);

            name = "countries";
            url = "{language}/countries/{country}";
            defaults = new
            {
                controller = "countries",
                action = "index",
                country = UrlParameter.Optional
            };
            routes.MapRoute(name, url, defaults);

            name = "info";
            url = "{language}/info/{id}";
            defaults = new
            {
                controller = "info",
                action = "index",
                id = UrlParameter.Optional
            };

            routes.MapRoute(name, url, defaults);
            name = "language";
            url = "{language}/{controller}/{action}/{id}";
            defaults = new {
                   controller = "excursion",
                   action = "index",
                   id = UrlParameter.Optional
            };

            object constraints = new {
                   language = @"\w\w(\-\w\w)?"
            };
            routes.MapRoute(name, url, defaults, constraints);
            name = "default";
            url = "{controller}/{action}/{id}";
            defaults = new {
                controller = "excursion",
                action = "index",
                id = UrlParameter.Optional
            };
            routes.MapRoute(name, url, defaults);

            name = "excursions";
            url = "{language}/excursion/{region}";
            defaults = new
            {
                controller = "excursion",
                action = "index",
                region = UrlParameter.Optional
            };
            routes.MapRoute(name, url, defaults);
        }
    }
}

