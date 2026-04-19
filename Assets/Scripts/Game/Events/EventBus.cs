using System.Collections.Generic;
using UnityEngine;

namespace Matso.Events
{
    public interface IObserver
    {
        void OnNotify(EventKey type, object data);
    }

    public static class EventBus
    {
        static Dictionary<EventKey, List<IObserver>> observers = new();

        public static void Register(EventKey type, IObserver observer)
        {
            if(!observers.ContainsKey(type)) observers.Add(type, new List<IObserver>());
            if(!observers[type].Contains(observer)) observers[type].Add(observer);
        }

        public static void Unregister(EventKey type, IObserver observer)
        {
            if(!observers.ContainsKey(type)) return;
            observers[type].Remove(observer);
        }

        public static void Publish(EventKey type, object data)
        {
            if(observers.ContainsKey(type)) observers[type].ForEach(observer => observer.OnNotify(type, data));
        }
    }
}
