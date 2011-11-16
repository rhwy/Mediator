using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mediator_Rx
{
    public class ObservableResultWrapper : IAsyncResult
    {
        private readonly ManualResetEvent asyncWaitHandler;
        private Exception exp;

        public ObservableResultWrapper(object asyncState)
        {
            AsyncState = asyncState;
            asyncWaitHandler = new ManualResetEvent(false);
        }

        internal void Success()
        {
            Complete();
        }

        internal void Fail(Exception exception)
        {
            exp = exception;
            Complete();
        }

        private void Complete()
        {
            IsCompleted = true;
            asyncWaitHandler.Set();
        }

        internal void CheckResult()
        {
            if (exp != null)
            {
                throw exp;
            }
        }
        public object AsyncState { get; private set; }


        public bool CompletedSynchronously
        {
            get { return false; }
        }

        public bool IsCompleted { get; private set; }


        #region IAsyncResult Members


        WaitHandle IAsyncResult.AsyncWaitHandle
        {
            get { return asyncWaitHandler; }
        }

        #endregion
    }
}
