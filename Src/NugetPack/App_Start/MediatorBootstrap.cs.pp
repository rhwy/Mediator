using System.Web.Mvc;
using StructureMap;

[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.MediatorBootsrap), "Start")]

namespace $rootnamespace$.App_Start {
    public static class MediatorBootsrap {
        public static void Start() {
        	//register the bus for simple messages (of string)
        	//dont forget to add a bus for each custom message
            MediatorBus.RegisterForType<string>();

            //add a route for Mediator in your routes configuration:
            /*
            routes.Add(
                "mediator",
                new Route(
                    "mediator/{type}/{name}",
                    new RouteValueDictionary(new { type = "string", name = "default" }),
                    new EventBusRouteHandler()));
	

            */
        }
    }
}