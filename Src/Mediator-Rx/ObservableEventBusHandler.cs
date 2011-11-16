using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ServiceStack.Text;
using Mediator;
using System.Reactive;
using System.Reactive.Linq;
using Mediator.Transport;

namespace Mediator_Rx
{
    public class ObservableEventBusHandler<T> : ObservableHttpHandler
    {
        public override IObservable<Unit> ProcessRequestAsObservable(HttpContext context)
        {
            var bus = EventBusManager.Current.GetFor<T>();

            var busObserver = Observable.FromEventPattern<MessageOf<T>>(
                    bus,        //on object 'bus'
                    "NewItem"   //connect to event named "NewItem"
                ).Select(               //from this observed event select
                    ep => ep.EventArgs    //the eventargs
                ).Take(ConfigurationManager.MAX_TAKE_EACH_TIME);              //take only 1 (understand : fire for every event)

            MessageOf<string> result = null;

            //Timeout method defines a timeout to wait for our observable, if timeout fires, it returns the alternative
            //sequence
            var resultUnit = busObserver.Timeout<MessageOf<T>>(
                //defines the time to wait
                     TimeSpan.FromSeconds(ConfigurationManager.MAX_WAIT_CONNECTION),
                //define the sequence to return if timeout fires, which is in our case an observable sequence
                //of only one element with the desired result
                     Observable.Return<MessageOf<T>>(new MessageOf<T>(default(T)))
                ).Select<MessageOf<T>, Unit>(
                //then define what to do with result (either  if it was fired by one or other method)
                    message =>
                    {
                        ProcessResult(context, message); //process message, ie : serialize/send to client
                        return Unit.Default;             //return void
                    }
                );

            return resultUnit;
        }
        private void ProcessResult(HttpContext context, MessageOf<T> result)
        {
            IEventBusResponse response = EventBusFactory.GetResponse<T>(context, result);
            response.Execute();
        }
    }
}
