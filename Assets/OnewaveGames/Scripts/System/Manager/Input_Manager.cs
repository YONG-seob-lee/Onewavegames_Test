using OnewaveGames.Scripts.EventHub;
using UnityEngine;
using Zenject;

namespace OnewaveGames.Scripts.System.Manager
{
    public class Input_Manager : MonoBehaviour, IManager
    {
        [Inject] public void Construct(SignalBus signalBus)
        {
        }

        public void InitManager(ManagerEventHub eventHub)
        {
        }

        public void DestroyManager()
        {
        }
    }
}