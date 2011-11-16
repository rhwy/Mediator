using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;

namespace Mediator_Rx
{
    
    /// <summary>
    /// routes.Add(
    //"mediator",
    //new Route(
    //    "bus/{type}/{name}", 
    //    new RouteValueDictionary(new { type="string", name="default"}),
    //    new EventBusRouteHandler()));
    /// </summary>
    public class ObservableEventBusRouteHandler : IRouteHandler
    {

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return ObservableEventBusRouter.ExtractHandler(requestContext);
        }
    }
}
