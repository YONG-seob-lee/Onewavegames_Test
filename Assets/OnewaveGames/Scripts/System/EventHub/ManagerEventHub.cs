using System;
using UnityEngine;

namespace OnewaveGames.Scripts.EventHub
{
    public class ManagerEventHub
    {
        public event Action<Vector3> OnInputReceived;
        public void BroadcastMove(Vector3 mousePosition)
        {
            OnInputReceived?.Invoke(mousePosition);
        }
    }

    public class Signal_InitializeManagers
    {
        public ManagerEventHub EventHub;

        public Signal_InitializeManagers(ManagerEventHub eventHub)
        {
            EventHub = eventHub;
        }
        
        
    }
}