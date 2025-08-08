using OnewaveGames.Scripts.System.Manager;
using OnewaveGames.Scripts.System.Table.TableData;
using UnityEngine;
using Zenject;

namespace OnewaveGames.Scripts.System.Installer
{
    public class TableInstaller : MonoInstaller
    {
        [SerializeField] private Table_Manager _tableManager;
        [SerializeField] private PathDirectory_DataTable _directoryTable;
        [SerializeField] private PrefabFile_DataTable _prefabDataTable;
        [SerializeField] private Skill_DataTable _skillDataTable;

        public override void InstallBindings()
        {
            // 테이블 매니저 주입
            Container.Bind<Table_Manager>().FromInstance(_tableManager).AsSingle();
            Container.Inject(this);
            
            // 데이터테이블 바인딩
            Container.Bind<PathDirectory_DataTable>().FromInstance(_directoryTable).AsSingle();
            _tableManager.RegisterTable(ETableType.Directory, _directoryTable);
            Container.Bind<PrefabFile_DataTable>().FromInstance(_prefabDataTable).AsSingle();
            _tableManager.RegisterTable(ETableType.PrefabFile, _prefabDataTable);
            Container.Bind<Skill_DataTable>().FromInstance(_skillDataTable).AsSingle();
            _tableManager.RegisterTable(ETableType.Skill, _skillDataTable);
        }
    }
}