using System.Collections.Generic;
using System.Linq;
using OnewaveGames.Scripts.EventHub;
using OnewaveGames.Scripts.System.Spawn;
using OnewaveGames.Scripts.System.Table.TableData;
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
        [Inject] private ManagerEventHub _eventHub;
        private readonly Dictionary<EUnitType, List<Actor_Base>> Units = new();
        
        [Inject] public void Construct(SignalBus signalBus)
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

        public Actor_Base CreateUnit(EUnitType unitType, int unitId = 0, Vector3 vector3 = default)
        {
            Actor_Base unit = null;
            
            switch (unitType)
            {
                case EUnitType.Player:
                    unit = CreateActor(unitType, unitId, vector3);
                    break;
                case EUnitType.Enemy:
                    unit = CreateActor(unitType, unitId, vector3);
                    break;
                default:
                    break;
            }

            if (unit != null)
            {
                unit.Initialize(unitType);

                if (!Units.ContainsKey(unitType))
                {
                    Units[unitType] = new List<Actor_Base>();
                }
                else
                {
                    Units[unitType].Add(unit);
                }

                return unit;
            }

            return null;
        }
        
        private Actor_Base CreateActor(EUnitType unitType, int unitId, Vector3 spawnLocation)
        {
            Table_Manager tableManager = (Table_Manager)GameManager.Instance.GetManager(EManager.Table);
            if (!tableManager)
            {
                Debug.Assert(false, "[TableManager] is not exist!!");
                return null;
            }
            PrefabFile_DataTable prefabTable = (PrefabFile_DataTable)tableManager.GetTable(ETableType.PrefabFile);
            if (!prefabTable)
            {
                Debug.Assert(false, "[Prefab Table] is not exist!!");
                return null;
            }
            
            string prefabPath = prefabTable.GetPath(unitId);
            if (string.IsNullOrEmpty(prefabPath))
            {
                Debug.LogError($"해당 유닛에 대한 프리팹 경로가 없습니다.");
                return null;
            }
            
            GameObject unitObject = Instantiate(Resources.Load<GameObject>(prefabPath));
            if (!unitObject)
            {
                Debug.LogError($"Resources.Load 실패: {prefabPath}");
                return null;
            }
            
            var unitFolder = GameObject.FindWithTag("Unit")?.transform;
            unitObject.transform.SetParent(unitFolder, false);
            return unitObject.GetComponent<Actor_Base>();
        }

        public List<T> FindObjects<T>() where T : Component
        {
            return FindObjectsByType<T>(FindObjectsSortMode.None).ToList();
        }
    }
}