using OnewaveGames.Scripts.EventHub;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OnewaveGames.Scripts.System.Controller
{
    public class PlayerController : MonoBehaviour
    {
        private Vector2 _mousePosition;
        private void Update()
        {
            _mousePosition = Mouse.current.position.ReadValue();

            if (Mouse.current.rightButton.isPressed)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(_mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("Floor")))
                {
                    Vector3 hitPoint = hit.point;
                    hitPoint.y = 0;
                    GlobalEventHub.EventHub.BroadcastMove(hitPoint);
                }
            }
        }
    }
}