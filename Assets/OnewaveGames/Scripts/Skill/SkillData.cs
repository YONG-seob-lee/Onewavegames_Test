using System.Collections.Generic;
using OnewaveGames.Scripts.Effect;
using OnewaveGames.Scripts.Skill.Indiacator;
using OnewaveGames.Scripts.System.Library;
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
    }
    
    [CreateAssetMenu(fileName = "NewSkillData", menuName = "Skills/Skill Data")]
    public class SkillData : ScriptableObject
    {
        public ESkillType skillType;
        
        [HideInInspector]
        public Skill_Entry SkillEntry;

        public List<SkillEffectSO> Effects = new List<SkillEffectSO>();

        public string SkillName => SkillEntry.SkillName;
        public float Cooldown => SkillEntry.Cooldown;
        public float ManaCost => SkillEntry.ManaCost;

        public void Initialize()
        {
            Skill_DataTable skillTable = (Skill_DataTable)SystemLibrary.GetTable(ETableType.Skill);
            if (skillTable == null)
            {
                Debug.LogError($"[Skill Data Table] is not exist!!");
                return;
            }
            
            SkillEntry = skillTable.GetEntry((int)skillType);

            foreach (var effect in Effects)
            {
                if (effect != null)
                {
                    effect.Initialize(SkillEntry);
                }
            }
        }

        public void OnUpdate(SkillIndicator skillIndicator, GameObject caster, GameObject target)
        {
            foreach (var effect in Effects)
            {
                effect.OnUpdate(skillIndicator, caster, target);
            }
        }
        public SkillEffectSO GetEffect(string skillKey)
        {
            foreach (var effect in Effects)
            {
                if (skillKey == effect.key.ToString())
                {
                    return effect;
                }
            }

            return null;
        }
    }
}