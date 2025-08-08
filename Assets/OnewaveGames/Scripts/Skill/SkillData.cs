using System.Collections.Generic;
using OnewaveGames.Scripts.Effect;
using OnewaveGames.Scripts.System.Table.TableData;
using UnityEngine;

namespace OnewaveGames.Scripts.Skill
{
    public interface ISkillEffect
    {
        void Apply(GameObject caster, GameObject target);
    }

    public enum ESkillType
    {
        None = 0,
        Grab = 1,
        // Add Other Skill
    }
    
    [CreateAssetMenu(fileName = "NewSkillData", menuName = "Skills/Skill Data")]
    public class SkillData : ScriptableObject
    {
        public ESkillType skillType;
        
        [HideInInspector]
        public Skill_Entry SkillEntry;

        public List<SkillEffectSO> Effects = new List<SkillEffectSO>();

        public string SkillName => SkillEntry.skillName;
        public float Cooldown => SkillEntry.cooldown;
        public float ManaCost => SkillEntry.manaCost;
        
        public void Initialize(Skill_Entry entry)
        {
            SkillEntry = entry;
        }
    }
}