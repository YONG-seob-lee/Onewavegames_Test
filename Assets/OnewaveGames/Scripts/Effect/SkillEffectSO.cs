using OnewaveGames.Scripts.Skill;
using OnewaveGames.Scripts.Skill.Indiacator;
using OnewaveGames.Scripts.System.Table.TableData;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OnewaveGames.Scripts.Effect
{
    public abstract class SkillEffectSO : ScriptableObject, ISkillEffect
    {
        public Key key;
        protected bool IsStart = false;

        public abstract void Initialize(Skill_Entry skillEntry);
        public abstract void Apply(GameObject caster, GameObject target);

        public abstract void OnStart(SkillIndicator skillIndicator, GameObject caster, GameObject target);
        public abstract void OnUpdate(SkillIndicator skillIndicator, GameObject caster, GameObject target);
        public abstract void OnEnd(SkillIndicator skillIndicator, GameObject caster);
    }
}