using OnewaveGames.Scripts.EventHub;
using UnityEngine;

namespace OnewaveGames.Scripts.Actor
{
    public class Actor_Player : Actor_Base
    {
        private float movespeed = 15f;
        private Vector3 _targetPosition;
        private bool _isMoving;
        
        public override void Initialize(EUnitType unitType)
        {
            base.Initialize(unitType);
            GlobalEventHub.EventHub.OnInputReceived += OnMove;
        }

        public void OnMove(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            _isMoving = true;
        }

        private void Update()
        {
            if (_isMoving)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, _targetPosition, movespeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
                {
                    _isMoving = false;
                }
            }
        }
    }
}