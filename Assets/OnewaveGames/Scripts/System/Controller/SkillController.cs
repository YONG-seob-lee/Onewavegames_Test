using System.Collections.Generic;
using OnewaveGames.Scripts.Ability;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OnewaveGames.Scripts.System.Controller
{
    public class SkillController : MonoBehaviour
    {
        private AbilitySystemComponent _asc;
        
        private List<Key> _skillKeys = new List<Key>();
        private Vector2 _mousePosition;
        private void Awake()
        {
            _asc = GetComponent<AbilitySystemComponent>();
            
            _skillKeys.Add(Key.Q);
            _skillKeys.Add(Key.E);
        }

        private void Update()
        {
            _mousePosition = Mouse.current.position.ReadValue();
            
            foreach (var key in _skillKeys)
            {
                if (Keyboard.current[key].wasPressedThisFrame)
                {
                    // Q키가 눌렸을 때
                    if (key == Key.Q)
                    {
                        Debug.Log("Q 키가 눌렸습니다. Grab 스킬 준비!");
                    }
                    _asc.StartActiveSkill(key.ToString(), _mousePosition);
                }

                if (Keyboard.current[key].wasReleasedThisFrame)
                {
                    // 키가 떼어졌을 때
                    _asc.EndActiveSkill(key.ToString(), _mousePosition);
                }
            }
        }
    }
}