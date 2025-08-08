using System.Collections.Generic;
using OnewaveGames.Scripts.System.Manager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace OnewaveGames.Scripts.System.Controller
{
    public class PlayerController : MonoBehaviour
    {
        private Input_Manager _inputManager;
        private Dictionary<string, ButtonControl> _actionToKeyMap;

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                _inputManager = (Input_Manager)GameManager.Instance.GetManager(EManager.Input);
            }
            
            _actionToKeyMap = new()
            {
                { "MoveUp", Keyboard.current.wKey },
                { "MoveDown", Keyboard.current.sKey},
                { "MoveLeft", Keyboard.current.aKey },
                { "MoveRight", Keyboard.current.dKey },
                { "Attack", Mouse.current.leftButton },
                { "UseSkill", Keyboard.current.qKey },
                { "Interact", Keyboard.current.eKey }
            };
        }

        void Update()
        {
            if (_actionToKeyMap == null)
                return;

            float deltaTime = Time.deltaTime;

        }
    }
}