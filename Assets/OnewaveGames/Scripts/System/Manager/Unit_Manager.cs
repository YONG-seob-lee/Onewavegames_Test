using OnewaveGames.Scripts.EventHub;
using UnityEngine;
using Zenject;

public enum EUnitType
{
    Player,
    Enemy,
}

namespace OnewaveGames.Scripts.System.Manager
{
    public class Unit_Manager : MonoBehaviour, IManager
    {
        public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            // ReSharper disable once Unity.NoNullPropagation
            GameManager.Instance?.RegisterManager(EManager.Unit, this);
        }

        public void InitManager(ManagerEventHub eventHub)
        {
        }

        public void DestroyManager()
        {
        }
    }
}