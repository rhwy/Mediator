using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediator
{
    /// <summary>
    /// This class defines an identifier for a bus that should be unique
    /// by design, the uniqueness is defined by the combination of a type 
    /// and a name.
    /// </summary>
    public class EventBusIdentifier
    {
        public string BusType { get; private set; }
        public string Name { get; private set; }

        public EventBusIdentifier(Type type)
            : this(string.Empty, type.Name)
        { }

        public EventBusIdentifier(string name)
            : this(name, string.Empty)
        { }

        public EventBusIdentifier(string name, Type type)
            : this(name, type.Name)
        {

        }

        public EventBusIdentifier(string name, string type)
        {
            BusType = string.IsNullOrEmpty(type) ? "dynamic" : type.ToLower();
            Name = string.IsNullOrEmpty(name) ? "default" : name;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", BusType, Name);
        }

        public string Key
        {
            get
            {
                return ToString();
            }
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            EventBusIdentifier other = obj as EventBusIdentifier;
            if (other == null)
            {
                return false;
            }
            return Key == other.Key;
        }

        public static EventBusIdentifier GetID<T>()
        {
            return GetID<T>(string.Empty);
        }

        public static EventBusIdentifier GetID(string name)
        {
            return GetID(name, string.Empty);
        }

        public static EventBusIdentifier GetID<T>(string name)
        {
            return GetID(name, typeof(T));
        }

        public static EventBusIdentifier GetID(string name, Type type)
        {
            return new EventBusIdentifier(name, type);
        }

        public static EventBusIdentifier GetID(string name, string type)
        {
            return new EventBusIdentifier(name, type);
        }
    }
}
