using OnewaveGames.Scripts.EventHub;
using OnewaveGames.Scripts.System.Manager;
using UnityEngine;
using Zenject;

namespace OnewaveGames.Scripts.System.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Scene_Manager _sceneManager;
        [SerializeField] private Table_Manager _tableManager;
        [SerializeField] private Unit_Manager _unitManager;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            // Manager Event Hub Signal
            Container.DeclareSignal<Signal_InitializeManagers>();
            
            // Bind ManagerEventHub
            var managerEventHub = new ManagerEventHub();
            GlobalEventHub.InitializeEventHub(managerEventHub);
            Container.Bind<ManagerEventHub>().FromInstance(managerEventHub).AsSingle();
            
            // Bind SkillEventHub
            var skillEventHub = new Skill_EventHub();
            GlobalEventHub.InitializeSkillHub(skillEventHub);
            Container.Bind<Skill_EventHub>().FromInstance(skillEventHub).AsSingle();

            // Bind Managers
            Container.Bind().FromInstance(_gameManager).AsSingle();
            Container.Bind().FromInstance(_sceneManager).AsSingle();
            Container.Bind().FromInstance(_tableManager).AsSingle();
            Container.Bind().FromInstance(_unitManager).AsSingle();
        }
    }
}