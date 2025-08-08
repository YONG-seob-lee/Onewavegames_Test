using System;
using OnewaveGames.Scripts.Skill;
using UnityEngine;

namespace OnewaveGames.Scripts.Effect
{
    [CreateAssetMenu(fileName = "PullEffect", menuName = "Skills/Effects/Pull Effect")]
    public class PullEffect : SkillEffectSO
    {
        public float pullSpeed = 5f;
        public override void Apply(GameObject caster, GameObject target)
        {
            if (target == null || caster == null)
            {
                Debug.LogError("Target or Caster is missing!");
                return;
            }
        }
    }
}