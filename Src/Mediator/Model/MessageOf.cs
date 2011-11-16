using System;
namespace Mediator
{
    [Serializable]
    public class MessageOf<T> : MessageOfBase
    {
        public T MessageItem { get; private set; }
        
        public MessageOf(T item)
        {
            MessageItem = item;
        }

        public override bool Equals(object obj)
        {
            MessageOf<T> compared = (MessageOf<T>) obj;
            if(compared == null)
            {
                return false;
            }

            return (MessageItem.Equals(compared.MessageItem)) && (TimeStamp == compared.TimeStamp);
        }
    }
}
