using OnewaveGames.Scripts.Skill.Indiacator;
using OnewaveGames.Scripts.System.Table.TableData;
using UnityEngine;

namespace OnewaveGames.Scripts.Effect
{
    [CreateAssetMenu(fileName = "PullEffect", menuName = "Skills/Effects/Pull Effect")]
    public class PullEffect : SkillEffectSO
    {
        public float pullSpeed = 5f;
        public override void Initialize(Skill_Entry skillEntry)
        {
            pullSpeed = skillEntry.ProjectileSpeed;
        }

        public override void Apply(GameObject caster, GameObject target)
        {
            if (target == null || caster == null)
            {
                Debug.LogError("Target or Caster is missing!");
                return;
            }
        }

        public override void OnStart(SkillIndicator skillIndicator, GameObject caster, GameObject target)
        {
            IsStart = true;
        }

        public override void OnUpdate(SkillIndicator skillIndicator, GameObject caster, GameObject target)
        {
            
            if (IsStart == false)
            {
                return;
            }
        }

        public override void OnEnd(SkillIndicator skillIndicator, GameObject target)
        {
            IsStart = false;
        }
    }
}