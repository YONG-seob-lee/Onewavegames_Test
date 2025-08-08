using System.Collections.Generic;
using OnewaveGames.Scripts.Skill;
using UnityEngine;

namespace OnewaveGames.Scripts.Ability
{
    public class AbilitySystemComponent : MonoBehaviour
    {
        public List<SkillData> grantedSkills = new List<SkillData>();

        public void TryActivateSkill(string skillName, GameObject target)
        {
            SkillData skillToActivate = grantedSkills.Find(s => s.SkillName == skillName);

            if (skillToActivate != null)
            {
                foreach (var effect in skillToActivate.Effects)
                {
                    effect.Apply(gameObject, target);
                }
            }
            else
            {
                Debug.LogWarning($"스킬을 찾을 수 없습니다 : {skillName}");
            }
        }
    }
}