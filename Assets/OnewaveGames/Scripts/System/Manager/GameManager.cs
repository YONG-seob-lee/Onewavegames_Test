using System.Collections.Generic;
using OnewaveGames.Scripts.EventHub;
using UnityEngine;
using Zenject;

namespace OnewaveGames.Scripts.System.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        // SignalBus
        [Inject] private SignalBus _signalBus;
        
        // EventHub
        [Inject] private ManagerEventHub _eventHub;

        // Manager
        private Dictionary<EManager, MonoBehaviour> _managers = new Dictionary<EManager, MonoBehaviour>();
        
        [Inject] private void Construct(SignalBus signalBus)
        {
            if (Instance != null && Instance != this)
            {
                DestroyImmediate(Instance);
                return;
            }

            Instance = this;
            _signalBus = signalBus;
        }

        private void Start()
        {
            StartTest();
        }

        private void StartTest()
        {
            _signalBus.Fire(new Signal_InitializeManagers(_eventHub));

            if (GetManager(EManager.Scene) is Scene_Manager sceneManager)
            {
                if (sceneManager.GetCurrentScene() != "PersistentScene")
                {
                    sceneManager.LoadGameScene();
                }

                StartCoroutine(sceneManager.LaunchTestScene());
            }
        }

        public void RegisterManager(EManager key, MonoBehaviour instance)
        {
            _managers[key] = instance;
        }
        public MonoBehaviour GetManager(EManager eManager)
        {
            return _managers.TryGetValue(eManager, out MonoBehaviour manager) ? manager : null;
        }
    }
}
