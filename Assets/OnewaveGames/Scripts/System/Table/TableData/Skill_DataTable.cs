using System.Collections.Generic;
using OnewaveGames.Scripts.Skill;
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
            TextAsset textAsset = SystemLibrary.CreateTextAsset("Table/Skill");
            if (textAsset != null)
            {
                SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }

        public void Clear() => DataMap.Clear();

        public Skill_Entry GetEntry(int skillKey)
        {
            return DataMap.GetValueOrDefault(skillKey);
        }

        public int GetDamage(ESkillType skillType)
        {
            Skill_Entry skillEntry = GetEntry((int)skillType);
            if (skillEntry == null)
            {
                return -1;
            }

            return skillEntry.Damage;
        }
    }

    public class Skill_Entry : Data
    {
        public readonly int Key;
        public readonly string SkillName;
        public readonly int Damage;
        public readonly float Cooldown;
        public readonly float ManaCost;
        public readonly float Range;
        public readonly float ProjectileSpeed;
    }
}