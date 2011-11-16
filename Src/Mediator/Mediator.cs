using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediator
{
    /// <summary>
    /// This class aims to be the principal and simplified entry point for the api
    /// </summary>
    public static class MediatorBus
    {
        //REGISTRATION
        public static void RegisterForType<T>()
        {
            EventBusManager.Current.Register<T>();
        }
        public static void RegisterForType<T>(string name)
        {
            EventBusManager.Current.Register<T>(name);
        }
        public static void Register(string name)
        {
            EventBusManager.Current.Register(name);
        }
        public static void RegisterDefault()
        {
            EventBusManager.Current.Register();
        }       
        
        //BUFFER
        public static IEnumerable BufferOf<T>(string name)
        {
            EventBus<T> bus = EventBusManager.Current.GetFor<T>(name);
            IEnumerable messages = bus.GetBuffer();
            return messages;
        }
        public static IEnumerable BufferOf<T>()
        {
            EventBus<T> bus = EventBusManager.Current.GetFor<T>();
            IEnumerable messages = bus.GetBuffer();
            return messages;
        }
        public static IEnumerable BufferOf(string name)
        {
            IEventBus bus = EventBusManager.Current.Get(name);
            IEnumerable messages = bus.GetBuffer();
            return messages;
        }

        //PUSH
        public static void Push<T>(object sender, string name, T message)
        {
            EventBus<T> bus = EventBusManager.Current.GetFor<T>(name);
            bus.OnNewItem(sender, message);
        }

        public static void Push<T>(object sender,T message)
        {
            EventBus<T> bus = EventBusManager.Current.GetFor<T>();
            bus.OnNewItem(sender, message);
        }


    }
}
