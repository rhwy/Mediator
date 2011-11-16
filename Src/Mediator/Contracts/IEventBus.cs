using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Mediator
{
    public interface IEventBus
    {
        long LastEventTimeStamp { get; }
        EventBusIdentifier ID { get; }
        IEnumerable GetBuffer();
    }
}
