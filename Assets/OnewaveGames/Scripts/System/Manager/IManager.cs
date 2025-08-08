using OnewaveGames.Scripts.EventHub;
using Zenject;

public enum EManager
{
    None = 0,
    Scene = 1,
    Input = 2,
    Table = 3,
    Unit = 4,
}

namespace OnewaveGames.Scripts.System.Manager
{
    public interface IManager
    {
        void Construct(SignalBus signalBus);
        void InitManager(ManagerEventHub eventHub);
        void DestroyManager();
    }
}