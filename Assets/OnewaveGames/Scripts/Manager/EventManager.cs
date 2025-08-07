using System;
using System.Collections.Generic;

namespace OnewaveGames.Scripts.Manager
{
    public class EventManager
    {
        private static Dictionary<Type, Delegate> eventDictionary = new Dictionary<Type, Delegate>();
        
        private static EventManager instance;

        public static EventManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventManager();
                }

                return instance;
            }
        }

        public static void Subscribe<T>(Action<T> listener) where T : struct
        {
            Type eventType = typeof(T);
            if (!eventDictionary.ContainsKey(eventType))
            {
                eventDictionary.Add(eventType, null);
            }

            eventDictionary[eventType] = (Action<T>)eventDictionary[eventType] + listener;
        }

        public static void Unsubcribe<T>(Action<T> listener) where T : struct
        {
            Type eventType = typeof(T);
            if (eventDictionary.ContainsKey(eventType))
            {
                eventDictionary[eventType] = (Action<T>)eventDictionary[eventType] - listener;
            }
        }

        public static void Public<T>(T eventData) where T : struct
        {
            Type eventType = typeof(T);
            if (eventDictionary.ContainsKey(eventType) && eventDictionary[eventType] != null)
            {
                (eventDictionary[eventType] as Action<T>)?.Invoke(eventData);
            }
        }
    }
}