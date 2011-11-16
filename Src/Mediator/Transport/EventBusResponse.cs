using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ServiceStack.Text;

namespace Mediator.Transport
{
    public  interface  IEventBusResponse
    {
        void Execute();
    }

    public class EventBusResponse<T> : IEventBusResponse
    {
        public string ResponseType { get; set; }
        public const string DEFAULT_RESPONSE_TYPE = "application/json";
        private HttpContext _context;
        private MessageOf<T> _result;
         
        public EventBusResponse(HttpContext context, MessageOf<T> result)
        {
            _context = context;
            _result = result;
           setResponseType(context);
        }

        public void Execute()
        {
            if (_context == null || !_context.Response.IsClientConnected || _context.Response.OutputStream == null || !_context.Response.OutputStream.CanWrite)
            {
                return;
            }
            doResponse(_context, _result);
        }

        private void setResponseType(HttpContext context)
        {
            if (context == null || context.Request.AcceptTypes == null || context.Request.AcceptTypes.Length < 1)
            {
                ResponseType = EventBusResponse<T>.DEFAULT_RESPONSE_TYPE;
            }
            else
            {
                ResponseType = context.Request.AcceptTypes[0];
            }
            ResponseType = ResponseType.ToLower();
        }

        private void doResponse(HttpContext context, MessageOf<T> result)
        {
            if (ResponseType == "application/json")
            {
                jsonResponse(context, result);
            }
            else if (ResponseType == "application/xml")
            {
                xmlResponse(context, result);
            }
            else if (ResponseType == "text/plain")
            {
                textResponse(context, result);
            }
            else if (ResponseType == "text/csv")
            {
                csvResponse(context, result);
            }
            else 
            {
                jsonResponse(context,result);
            }
        }

        private void jsonResponse(HttpContext context, MessageOf<T> result)
        {
            var jsonResult = JsonSerializer.SerializeToString<MessageOf<T>>(result);

            context.Response.AddHeader("Content-Type", "application/json");
            context.Response.Write(jsonResult);
            context.Response.End();
        }
        private void csvResponse(HttpContext context, MessageOf<T> result)
        {
            var csvResult = CsvSerializer.SerializeToString<MessageOf<T>>(result);
            context.Response.AddHeader("Content-Type", "text/csv");
            context.Response.Write(csvResult);
            context.Response.End();
        }
        private void xmlResponse(HttpContext context, MessageOf<T> result)
        {
            var xmlResponse = XmlSerializer.SerializeToString<MessageOf<T>>(result);
            context.Response.AddHeader("Content-Type", "application/xml");
            context.Response.Write(xmlResponse);
            context.Response.End();
        }
        private void textResponse(HttpContext context, MessageOf<T> result)
        {
            string stringResult = result.MessageItem.Equals(null) ? result.MessageItem.ToString():string.Empty;
            context.Response.AddHeader("Content-Type", "text/plain");
            context.Response.Write(stringResult);
            context.Response.End();
        }
    }
}
