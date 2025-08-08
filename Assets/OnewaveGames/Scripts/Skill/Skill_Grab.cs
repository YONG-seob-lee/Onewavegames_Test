using OnewaveGames.Scripts.EventHub;
using UnityEngine;

namespace OnewaveGames.Scripts.Skill
{
    public class Skill_Grab : Skill_Base
    {
        public override bool ApplySkill(Actor_Base source, Actor_Base target)
        {
            Debug.Log($"{source.name}이(가) 스킬 시전 이벤트를 보냅니다.");

            return true;
        }

        public void Execute(Actor_Base source, Actor_Base target)
        {
            Debug.Log($"{source.name}의 {SkillDataTable.name}스킬이 발동됩니다.");
            ApplySkill(source, target);
        }
    }
}