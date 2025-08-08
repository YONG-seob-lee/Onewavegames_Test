using System.Collections;
using OnewaveGames.Scripts.EventHub;
using UnityEngine;
using UnityEngine.SceneManagement;
using OnewaveGames.Scripts.System.State;
using OnewaveGames.Scripts.System.State.SceneState;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

public enum ESceneType
{
    Test = 0,
}

namespace OnewaveGames.Scripts.System.Manager
{
    public class Scene_Manager : MonoBehaviour, IManager
    {
        private StateMachine _sceneStateMachine;
        [Inject] public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            // ReSharper disable once Unity.NoNullPropagation
            GameManager.Instance?.RegisterManager(EManager.Scene, this);
        }

        public void InitManager(ManagerEventHub eventHub)
        {
            _sceneStateMachine = new StateMachine();

            RegisterScene(ESceneType.Test);
            _sceneStateMachine.ChangeState((int)ESceneType.Test);
        }

        public void Update()
        {
            if (_sceneStateMachine == null)
            {
                return;
            }
            
            _sceneStateMachine.Update();
        }

        public void DestroyManager()
        {
        }

        public void RegisterScene(ESceneType sceneType)
        {
            _sceneStateMachine.RegisterState<State_TestScene>((int)sceneType, sceneType.ToString());
        }
        
        public string GetCurrentScene()
        {
            return SceneManager.GetActiveScene().name;
        }
        
        public void LoadGameScene()
        {
            SceneManager.LoadScene("PersistentScene");
        }
        

        public IEnumerator LaunchTestScene()
        {
            string nextSceneName = "TestScene";
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);

            yield return handle;
            
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Scene loadedScene = handle.Result.Scene;
                if (SceneManager.SetActiveScene(loadedScene))
                {
                    _sceneStateMachine.ChangeState((int)ESceneType.Test);       
                }
            }
            else
            {
                Debug.LogError($"[SceneLoad] Failed to load scene: ");
            }
        }
    }
}