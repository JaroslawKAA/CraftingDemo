using System;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Core.GameEvents
{
    public static class EventManager
    {
        static Dictionary<Type, List<EventListener>> events = new();

        public static void RegisterListener<T>(EventListener listener) where T : EventBase
        {
            Type eventType = typeof(T);
            if (!events.ContainsKey(eventType))
            {
                events[eventType] = new List<EventListener>();
            }
            events[eventType].Add(listener);
        }

        public static void UnregisterListener<T>(EventListener listener) where T : EventBase
        {
            Type eventType = typeof(T);
            if (events.ContainsKey(eventType))
            {
                events[eventType].Remove(listener);
            }
        }

        public static void TriggerEvent<T>(T eventInstance) where T : EventBase
        {
            Type eventType = typeof(T);
            
            Debug.Log($"EventManager.TriggerEvent {eventType}");
            
            if (events.TryGetValue(eventType, out List<EventListener> eventListeners))
            {
                foreach (EventListener listener in eventListeners)
                {
                    listener.OnEvent(eventInstance);
                }
            }
        }
    }
}