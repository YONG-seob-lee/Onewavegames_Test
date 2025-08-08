using OnewaveGames.Scripts.Skill;
using UnityEngine;

namespace OnewaveGames.Scripts.Effect
{
    public abstract class SkillEffectSO : ScriptableObject, ISkillEffect
    {
        public abstract void Apply(GameObject caster, GameObject target);
    }
}