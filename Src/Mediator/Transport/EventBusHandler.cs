using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Mediator
{
    public interface  IEventBusHandler {}


    public class EventBusHandler<T> : IEventBusHandler, IHttpAsyncHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
           get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
           throw new UnauthorizedAccessException("Synchronous method not available on this async handler"); 
        }

        #endregion

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            
            EventBusOperation<T> asynch = new EventBusOperation<T>(cb, context, extraData);
            asynch.StartAsyncWork();
            return asynch;
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            EventBusOperation<T> op = (EventBusOperation<T>)result;
            if (op != null && !op.ResponseCompleted)
            {
                op.Close();
            }
        }
    }
}
