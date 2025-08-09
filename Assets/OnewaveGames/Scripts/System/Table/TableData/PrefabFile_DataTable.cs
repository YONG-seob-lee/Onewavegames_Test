using System.Collections.Generic;
using OnewaveGames.Scripts.System.Library;
using OnewaveGames.Scripts.System.Manager;
using UnityEngine;

namespace OnewaveGames.Scripts.System.Table.TableData
{
    [CreateAssetMenu(fileName = "PrefabFile", menuName = "Tables/PrefabFile Data Table")]
    public class PrefabFile_DataTable : ScriptableObject, IDataTable
    {
        public Dictionary<int, PrefabFile_Entry> DataMap = new();

        public void RegisterData()
        {
            TextAsset textAsset = SystemLibrary.CreateTextAsset("Table/PrefabFile");
            if (textAsset != null)
            {
                SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }

        public void Clear() => DataMap.Clear();
        public string GetPath(int prefabKey)
        {
            if (DataMap.TryGetValue(prefabKey, out var prefabFileEntry) == false)
            {
                return null;
            }
            
            Table_Manager tableManager = (Table_Manager)GameManager.Instance.GetManager(EManager.Table);
            if (!tableManager)
            {
                return null;
            }
            
            PathDirectory_DataTable directoryTable = (PathDirectory_DataTable)tableManager.GetTable(ETableType.Directory);
            if (directoryTable != null)
            {
                string directory = directoryTable.GetDirectory(prefabFileEntry.Directory_Key);
                return directory + "/" + prefabFileEntry.Prefab_FileName;
            }

            return null;
        }
    }
    
    public class PrefabFile_Entry : Data
    {
        public readonly int Key;
        public readonly int Type;
        public readonly int Directory_Key;
        public readonly string Prefab_FileName;
    }
}