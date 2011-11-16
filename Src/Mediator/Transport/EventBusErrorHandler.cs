using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ServiceStack.Text;

namespace Mediator.Transport
{
    public class EventBusErrorHandler : IEventBusHandler, IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public string Message { get; set; }

        public EventBusErrorHandler(string message)
        {
            Message = message;
        }

        public EventBusErrorHandler()
        {
            
        }

        public void ProcessRequest(HttpContext context)
        {
            string message = "event bus server error";
            if (!string.IsNullOrEmpty(Message))
            {
                message = Message;
            } 
            else if(context.Items.Contains("MEDIATOR_ERROR_MESSAGE"))
            {
                message = (string)context.Items["MEDIATOR_ERROR_MESSAGE"];
            } 

            ProcessResult<string>(context,new MessageOf<string>(message));
        }

        private void ProcessResult<T>(HttpContext context, MessageOf<T> result)
        {
            //serialize
            var jsonResult = JsonSerializer.SerializeToString<MessageOf<T>>(result);

            //send
            context.Response.AddHeader("Content-Type", "application/json");
            context.Response.StatusCode = 400;
            context.Response.Write(jsonResult);
            context.Response.End();
        }
    }
}
