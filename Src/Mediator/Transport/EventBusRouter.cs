using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Reflection;

namespace Mediator.Transport
{
    
    public class EventBusRouter
    {
        public static EventBusIdentifier ExtractIdentifier(RequestContext requestContext)
        {
            string type = requestContext.RouteData.Values["type"] as string;
            string name = requestContext.RouteData.Values["name"] as string;

            if (string.IsNullOrEmpty(type) && string.IsNullOrEmpty(name))
            {
                return null;
            }

            return new EventBusIdentifier(name,type);
        }

        public static IHttpHandler ExtractHandler(RequestContext requestContext)
        {
            return ExtractHandler(ExtractIdentifier(requestContext));
        }

        public static IHttpHandler ExtractHandler(EventBusIdentifier id)
        {
            if (id == null)
            {
                return new EventBusErrorHandler("bus identifier is null");
            }
            if (!EventBusManager.Current.HasBus(id))
            {
                return new EventBusErrorHandler(string.Format("Bus id [{0}] was not registred",id.Key));
            }
            Type specialType = EventBusManager.Current.GetTypeForKey(id);
            if(specialType == null)
            {
                return new EventBusErrorHandler(string.Format("the Type for the bus id [{0}] was not found",id.Key));
            }
            Type eventBusHandlerGeneric = typeof (EventBusHandler<>);
            Type[] typeArgs = { specialType };
            var eventBusHandler = eventBusHandlerGeneric.MakeGenericType(typeArgs);
            IHttpHandler result = (IHttpHandler)Activator.CreateInstance(eventBusHandler);
            return result;
        }
    }
}
