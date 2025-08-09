using System.Collections.Generic;
using OnewaveGames.Scripts.Effect;
using OnewaveGames.Scripts.Skill;
using OnewaveGames.Scripts.Skill.Indiacator;
using UnityEngine;

namespace OnewaveGames.Scripts.Ability
{
    public class AbilitySystemComponent : MonoBehaviour
    {
        public List<SkillData> skillDatas = new List<SkillData>();
        private SkillIndicator skillIndicator;
        
        public void Awake()
        {
            foreach (var skillData in skillDatas)
            {
                if (skillData != null)
                {
                    skillData.Initialize();
                }
            }

            skillIndicator = GetComponent<SkillIndicator>();
        }

        private void Update()
        {
            foreach (var skillData in skillDatas)
            {
                if (skillData != null)
                {
                    skillData.OnUpdate(skillIndicator, gameObject, null);
                }
            }
        }

        public void StartActiveSkill(string skillKey)
        {
            SkillEffectSO skillEffect = GetSkillEffect(skillKey);
            if (skillEffect != null)
            {
                skillEffect.OnStart(skillIndicator, gameObject, null);
            }
        }

        private SkillEffectSO GetSkillEffect(string skillKey)
        {
            foreach (var skillData in skillDatas)
            {
                return skillData.GetEffect(skillKey);
            }

            return null;
        }

        public void EndActiveSkill(string skillKey)
        {
            SkillEffectSO skillEffect = GetSkillEffect(skillKey);
            if (skillEffect != null)
            {
                skillEffect.OnEnd(skillIndicator, gameObject);
            }
        }
        public void TryActivateSkill(string skillName, GameObject target)
        {
            SkillData skillToActivate = skillDatas.Find(s => s.SkillName == skillName);

            if (skillToActivate != null)
            {
                foreach (var effect in skillToActivate.Effects)
                {
                    //effect.Apply(gameObject, target);
                }
            }
            else
            {
                Debug.LogWarning($"스킬을 찾을 수 없습니다 : {skillName}");
            }
        }
    }
}