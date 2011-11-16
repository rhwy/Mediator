using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Mediator.Sample.Site.Models;
using Mediator_Rx;

namespace Mediator.Sample.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Navigation
            routes.MapRoute(
                "Home", // default route
                "Home/{action}",
                new { controller = "home", action = "Index" } // Parameter defaults
            );

            //Message push
            routes.MapRoute(
                "Push", // default route
                "MediatorNotifier/{action}",
                new { controller = "MediatorNotifier", action = "Index" } // Parameter defaults
            ); 
            //Messages bus
            routes.Add(
                "mediator",
                new Route(
                    "mediator/{type}/{name}",
                    new RouteValueDictionary(new { type = "string", name = "default" }),
                    new EventBusRouteHandler()));

            //Messages bus
            routes.Add(
                "mediator-rx",
                new Route(
                    "mrx/{type}/{name}",
                    new RouteValueDictionary(new { type = "string", name = "default" }),
                    new ObservableEventBusRouteHandler()));

            //root navigation
            routes.MapRoute(
                "root", // default route
                "",
                new { controller="home",action = "Index" } // Parameter defaults
            ); 
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            MediatorBus.RegisterForType<string>();
            MediatorBus.RegisterForType<ChatMessage>();
        }
    }
}