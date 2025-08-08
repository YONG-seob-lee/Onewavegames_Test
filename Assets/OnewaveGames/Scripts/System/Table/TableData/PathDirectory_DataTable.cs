using System.Collections.Generic;
using OnewaveGames.Scripts.System.Library;
using UnityEngine;

namespace OnewaveGames.Scripts.System.Table.TableData
{
    [CreateAssetMenu(fileName = "PathDirectory", menuName = "Tables/PathDirectory Data Table")]
    public class PathDirectory_DataTable : ScriptableObject, IDataTable
    {
        public Dictionary<int, Directory_Entry> DataMap = new();
        public void RegisterData()
        {
            TextAsset textAsset = SystemLibrary.CreateTextAsset("Table/PathDirectory");
            if (textAsset != null)
            {
                SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }

        public void Clear() => DataMap.Clear();

        public string GetDirectory(int directoryKey)
        {
            if (DataMap.TryGetValue(directoryKey, out var directoryEntry))
            {
                return directoryEntry.Directory;
            }

            return null;
        }
    }

    public class Directory_Entry : Data
    {
        public readonly int Key;
        public readonly string Directory;
    }
}