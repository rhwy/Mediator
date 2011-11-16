namespace Mediator
{
    using System;

    [Serializable]
    public class MessageOfBase : EventArgs
    {
        public long TimeStamp { get; private set; }

        public MessageOfBase()
        {
            TimeStamp = DateTime.Now.ToUnixTicks();
        }
    } 

}
