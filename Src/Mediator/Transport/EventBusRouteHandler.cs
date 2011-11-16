using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;
using Mediator.Transport;

namespace Mediator
{
    /// <summary>
    /// routes.Add(
                //"mediator",
                //new Route(
                //    "bus/{type}/{name}", 
                //    new RouteValueDictionary(new { type="string", name="default"}),
                //    new EventBusRouteHandler()));
    /// </summary>
    public class EventBusRouteHandler : IRouteHandler
    {
       
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return EventBusRouter.ExtractHandler(requestContext);
        }
    }
}
