using System;
using System.Collections.Generic;

namespace OnewaveGames.Scripts.EventHub
{
    public class Skill_EventHub
    {
        private static Dictionary<Type, List<Delegate>> _listeners = new();
        
        public void Subscribe<T>(Action<T> callback)
        {
            var type = typeof(T);
            if (!_listeners.ContainsKey(type))
            {
                _listeners.Add(type, new List<Delegate>());
            }
            _listeners[type].Add(callback);
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            var type = typeof(T);
            if (!_listeners.ContainsKey(type))
            {
                return;
            }
            _listeners[type].Remove(callback);
        }
        
        public void Publish<T>(T eventData)
        {
            Type eventType = typeof(T);
            if (_listeners.ContainsKey(eventType) && _listeners[eventType] != null)
            {
                (_listeners[eventType] as Action<T>)?.Invoke(eventData);
            }
        }
    }

    public class HitEvent
    {
        public Actor_Base Attacker;
        public Actor_Base Target;
        public Skill_Base Skill;

        public HitEvent(Actor_Base attacker, Actor_Base target, Skill_Base skill)
        {
            Attacker = attacker;
            Target = target;
            Skill = skill;
        }
    }
}