using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using Mediator;
using Mediator.Transport;

namespace Mediator_Rx
{
    public class ObservableEventBusRouter
    {
        public static IHttpHandler ExtractHandler(RequestContext requestContext)
        {
            return ExtractHandler(EventBusRouter.ExtractIdentifier(requestContext));
        }

        public static IHttpHandler ExtractHandler(EventBusIdentifier id)
        {
            if (id == null)
            {
                return new EventBusErrorHandler("bus identifier is null");
            }
            if (!EventBusManager.Current.HasBus(id))
            {
                return new EventBusErrorHandler(string.Format("Bus id [{0}] was not registred", id.Key));
            }
            Type specialType = EventBusManager.Current.GetTypeForKey(id);
            if (specialType == null)
            {
                return new EventBusErrorHandler(string.Format("the Type for the bus id [{0}] was not found", id.Key));
            }
            Type eventBusHandlerGeneric = typeof(ObservableEventBusHandler<>);
            Type[] typeArgs = { specialType };
            var eventBusHandler = eventBusHandlerGeneric.MakeGenericType(typeArgs);
            IHttpHandler result = (IHttpHandler)Activator.CreateInstance(eventBusHandler);
            return result;
        }
    }
}
