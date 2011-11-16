using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading;
using System.Reactive;

namespace Mediator_Rx
{
    public abstract class ObservableHttpHandler : IHttpAsyncHandler
    {
        public abstract IObservable<Unit> ProcessRequestAsObservable(HttpContext context);


        #region IHttpAsyncHandler Members

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            var asyncResult = new ObservableResultWrapper(extraData);
            var observable = ProcessRequestAsObservable(context);

            observable.Subscribe(
                _ => { },
                e =>
                {
                    asyncResult.Fail(e);
                    cb(asyncResult);
                },
                () =>
                {
                    asyncResult.Success();
                    cb(asyncResult);
                });
            return asyncResult;
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            ( (ObservableResultWrapper)result ).CheckResult();
        }

        #endregion

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var gate = new ManualResetEvent(false);
            AsyncCallback cb = ( _ => gate.Set() );
            ( (IHttpAsyncHandler)this ).BeginProcessRequest(context, cb, null);
            gate.WaitOne();
        }

        #endregion
    }
}
