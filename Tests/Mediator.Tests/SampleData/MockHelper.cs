using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Web;
using System.Web.Routing;

namespace Mediator.Tests.SampleData
{
    public static class MockHelper
    {
        public static HttpContextBase GetHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            
            return context.Object;
        }

        public static RequestContext GetRouterRequestContext(string path, string type, string name)
        {
            HttpContextBase httpContext = new StubHttpContextForRouting(path);
            RouteData routeData = new RouteData();
            routeData.Values.Add("type", type);
            routeData.Values.Add("name", name);
            RequestContext requestContext = new RequestContext(httpContext, routeData);
            return requestContext;
        }

    }
}
