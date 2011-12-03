using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediator
{
    public class EventBusManager
    {
        private static readonly EventBusManager _current = new EventBusManager();

        public static EventBusManager Current
        {
            get { return _current; }
        }

        private readonly Dictionary<EventBusIdentifier, IEventBus> _eventBusList = new Dictionary<EventBusIdentifier, IEventBus>();
        private readonly Dictionary<EventBusIdentifier, Type> _eventBusTypeName = new Dictionary<EventBusIdentifier, Type>();
        private readonly object _syncLock = new object();

        //typed registration with no name
        public void Register<T>()
        {
            Register(new EventBus<T>());    
        }

        //type registration with name
        public void Register<T>(string name)
        {
            Register(new EventBus<T>(name));
        }

        //registration from bus
        public void Register<T>(EventBus<T> bus )
        {
            lock (_syncLock)
            {
                if (!_eventBusList.ContainsKey(bus.ID) && !_eventBusTypeName.ContainsKey(bus.ID))
                {
                    _eventBusList.Add(bus.ID, bus);
                    _eventBusTypeName.Add(bus.ID, typeof (T));
                } 
            }
        }
    
        //untyped registration with no name
        public void Register()
        {
            Register(string.Empty);
        }

        //untyped registration with no name
        public void Register(string name)
        {
            Register(new EventBus<object>(name));
        }
        
        public IEventBus Get(string name)
        {
            EventBusIdentifier id = EventBusIdentifier.GetID(name);
            if (_eventBusList.ContainsKey(id))
            {
                return _eventBusList[id];

            }
            return null;
        }

        public EventBus<T> GetFor<T>()
        {
            EventBusIdentifier id = EventBusIdentifier.GetID<T>();
            if (_eventBusList.ContainsKey(id))
            {
                return (EventBus<T>)_eventBusList[id];
                
            }
            return null;
        }

        public EventBus<T> GetFor<T>(string name)
        {
            EventBusIdentifier id = new EventBusIdentifier(name,typeof(T));
            if (_eventBusList.ContainsKey(id))
            {
                return (EventBus<T>)_eventBusList[id];

            }
            return null;
        }

        public Type GetTypeForKey(EventBusIdentifier id)
        {
            if (_eventBusTypeName.ContainsKey(id))
            {
                return _eventBusTypeName[id];
            }
            return null;
        }

        public  bool HasBus(EventBusIdentifier id)
        {
            return _eventBusList.ContainsKey(id);
        }
    }
}
