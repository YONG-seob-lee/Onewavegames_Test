using System;
using System.Collections.Generic;
using OnewaveGames.Scripts.Skill;
using UnityEngine;

namespace OnewaveGames.Scripts.EventHub
{
    public class Skill_EventHub
    {
        private static Dictionary<Type, List<Delegate>> _listeners = new();
        public event Action<ESkillType> OnSkillReleased;
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
                var listenersToInvoke = new List<Delegate>(_listeners[eventType]);
                foreach (var listener in listenersToInvoke)
                {
                    if (listener is Action<T> action)
                    {
                        action.Invoke(eventData);
                    }
                }
            }
        }

        public void BroadcastSkillReleased(ESkillType skillType)
        {
            OnSkillReleased?.Invoke(skillType);
        }
    }

    public class HitEvent
    {
        public GameObject Attacker;
        public GameObject Target;
        public ESkillType SkillType;

        public HitEvent(GameObject attacker, GameObject target, ESkillType skillType)
        {
            Attacker = attacker;
            Target = target;
            SkillType = skillType;
        }
    }
}