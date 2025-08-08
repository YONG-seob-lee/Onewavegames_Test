using OnewaveGames.Scripts.EventHub;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace OnewaveGames.Scripts.System.Manager
{
    public class Scene_Manager : MonoBehaviour, IManager
    {
        public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            // ReSharper disable once Unity.NoNullPropagation
            GameManager.Instance?.RegisterManager(EManager.Scene, this);
        }

        public void InitManager(ManagerEventHub eventHub)
        {
        }

        public void DestroyManager()
        {
        }
        
        public string GetCurrentScene()
        {
            return SceneManager.GetActiveScene().name;
        }
        
        public void LoadGameScene()
        {
            SceneManager.LoadScene("PersistentScene");
        }
    }
}