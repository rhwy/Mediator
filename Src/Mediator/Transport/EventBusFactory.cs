using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Mediator.Transport
{
    //poor man injection factory
    public static class EventBusFactory
    {
        public static IEventBusResponse GetResponse<T>(HttpContext context, MessageOf<T> result)
        {
            return new EventBusResponse<T>(context, result);
        }


    }
}
