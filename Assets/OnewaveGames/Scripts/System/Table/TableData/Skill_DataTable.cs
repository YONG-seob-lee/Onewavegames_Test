using System.Collections.Generic;
using OnewaveGames.Scripts.System.Library;
using UnityEngine;

namespace OnewaveGames.Scripts.System.Table.TableData
{
    [CreateAssetMenu(fileName = "Skill", menuName = "Tables/Skill Data Table")]
    public class Skill_DataTable : ScriptableObject, IDataTable
    {
        public Dictionary<int, Skill_Entry> DataMap = new();
        public void RegisterData()
        {
            TextAsset textAsset = SystemLibrary.CreateTextAsset("Tables/Mode");
            if (textAsset != null)
            {
                SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }

        public void Clear() => DataMap.Clear();
    }

    public class Skill_Entry : Data
    {
        public readonly string skillName;
        public readonly float cooldown;
        public readonly float manaCost;
        public readonly float range;
        public readonly float projectileSpeed;
    }
}