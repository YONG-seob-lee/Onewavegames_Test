using System.Collections.Generic;
using OnewaveGames.Scripts.EventHub;
using OnewaveGames.Scripts.System.Table;
using OnewaveGames.Scripts.System.Table.TableData;
using UnityEngine;
using Zenject;

public enum ETableType
{
    None = 0,
    Directory = 1,
    PrefabFile = 2,
    Unit = 3,
    Skill = 4,
}

namespace OnewaveGames.Scripts.System.Manager
{
    public class Table_Manager : MonoBehaviour, IManager
    {
        private Dictionary<ETableType, IDataTable> _tableMap = new();

        [Inject] public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            GameManager.Instance?.RegisterManager(EManager.Table, this);
        }

        public void RegisterTable(ETableType tableType, IDataTable table)
        {
            _tableMap[tableType] = table;
        }

        public void InitManager(ManagerEventHub eventHub)
        {
            RegisterTableData();
        }

        private void RegisterTableData()
        {
            foreach (var table in _tableMap)
            {
                table.Value.RegisterData();
            }
        }
        
        public string GetPath(int prefabKey)
        {
            PrefabFile_DataTable prefabTable = (PrefabFile_DataTable)GetTable(ETableType.PrefabFile);
            if (prefabTable)
            {
                return prefabTable.GetPath(prefabKey);
            }

            return null;
        }

        public IDataTable GetTable(ETableType tableType)
        {
            return _tableMap.GetValueOrDefault(tableType);
        }

        public void DestroyManager()
        {
            
        }
    }
}