using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Mediator
{
    public class EventBus<T> : IEventBus
    {
        public long LastEventTimeStamp { get; set; }
        public EventBusIdentifier ID {get;private set;}

        public event EventHandler<MessageOf<T>> NewItem; 

        public Queue<MessageOf<T>> MessageBuffer = new Queue<MessageOf<T>>();
        private object _lockQueue = new object();


        public EventBus():this(string.Empty)
        {
        
        }
        public EventBus(string name)
        {
            ID = new EventBusIdentifier(name,typeof(T));
            NewItem += new EventHandler<MessageOf<T>>(Queue);
        }

        private void Queue(object sender, MessageOf<T> message)
        {
            lock (_lockQueue)
            {
                while (MessageBuffer.Count > ConfigurationManager.MAX_ITEMS_IN_QUEUE)
                {
                    MessageBuffer.Dequeue();
                }
                MessageBuffer.Enqueue(message);
            }
        }


        public IEnumerable GetBuffer()
        {
            var buffer = new List<MessageOf<T>>();
            lock(_lockQueue)
            {
                return MessageBuffer
                    .Where(m=>m.MessageItem != null)
                    .OrderByDescending(m=>m.TimeStamp)
                    .ToList();
            }
            return buffer;
        }

        public IEnumerable GetBuffer(long ticks)
        {
            var buffer = new List<MessageOf<T>>();
            lock(_lockQueue)
            {
                return MessageBuffer.Where(m => m.TimeStamp >= ticks).ToList();
            }
            return buffer;
        }

        public IEnumerable GetBuffer(DateTime time)
        {
            var buffer = new List<MessageOf<T>>();
            lock (_lockQueue)
            {
                return MessageBuffer.Where(m => m.TimeStamp >= time.ToUnixTicks()).ToList();
            }
            return buffer;
        }

        public int Subscribers
        {
            get
            {
                if (NewItem == null)
                {
                    return 0;
                }
                return NewItem.GetInvocationList().Length;
            }
        }
        public void OnNewItem(object sender, T item)
        {
            if (NewItem != null && NewItem.GetInvocationList().Length > 0)
            {
                var message = new MessageOf<T>(item);
                NewItem(sender, message);
                LastEventTimeStamp = message.TimeStamp;
            }
            
        }
    }
}
