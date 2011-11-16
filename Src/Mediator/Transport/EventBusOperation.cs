using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Mediator.Transport;
using ServiceStack.Text;
using System.Threading;

namespace Mediator
{
    public class EventBusOperation<T> : IAsyncResult
    {
        private bool _completed;
        private bool _responseCompleted;
        private Object _state;
        private AsyncCallback _callback;
        private HttpContext _context;

        bool IAsyncResult.IsCompleted
        {
            get { return _completed; }
        }
        WaitHandle IAsyncResult.AsyncWaitHandle
        {
            get { return null; }
        }
        Object IAsyncResult.AsyncState
        {
            get { return _state; }
        }
        bool IAsyncResult.CompletedSynchronously
        {
            get { return false; }
        }

        public Action<HttpContext,MessageOf<T>> ProcessAction {get;set;}

        public bool ResponseCompleted
        {
            get { return _responseCompleted; }
        }
        public EventBusOperation(AsyncCallback callback, HttpContext context, Object state)
        {
            _callback = callback;
            _context = context;
            _state = state;
            _completed = false;
            _responseCompleted = false;
            ProcessAction = ProcessResult;
        }

        public void StartAsyncWork()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartAsyncTask), null);
        }

        private void StartAsyncTask(Object workItemState)
        {
            var bus = EventBusManager.Current.GetFor<T>();
            EventHandler<MessageOf<T>> onEvent = ProcessOnEvent;

            bus.NewItem += onEvent;

            Timer t = new Timer(
                (e) =>
                {
                    bus.NewItem -= onEvent;
                    ProcessOnEvent(this, new MessageOf<T>(default(T)));
                }, null, ConfigurationManager.MAX_WAIT_CONNECTION, Timeout.Infinite);

        }

        void ProcessOnEvent(object sender, EventArgs a)
        {
            if (!_completed)
            {
                ProcessAction(_context, (MessageOf<T>)a);
                _completed = true;
                _callback(this);
            }

        }

        private void ProcessResult(HttpContext context, MessageOf<T> result)
        {
            IEventBusResponse response = EventBusFactory.GetResponse<T>(context, result);
            response.Execute();
            _responseCompleted = true;
        }

        public void Close()
        {
            if (_completed && !_responseCompleted  && _context != null)
            {
                _context.Response.End();
            }
        }
    }
}
