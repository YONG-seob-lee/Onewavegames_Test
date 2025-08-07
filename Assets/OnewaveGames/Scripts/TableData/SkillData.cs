using UnityEngine;

namespace OnewaveGames.Scripts.TableData
{
    [CreateAssetMenu(fileName = "New Skill Data", menuName = "Skill System / Skill Data")]
    public class SkillData
    {
        public readonly string skillName;
        public readonly float cooldown;
        public readonly float manaCost;
        public readonly float range;
        public readonly float projectileSpeed;
    }
}