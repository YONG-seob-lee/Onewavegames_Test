using OnewaveGames.Scripts.EventHub;
using UnityEngine;

namespace OnewaveGames.Scripts.Projectile
{
    public class Projectile
    {
        private Actor_Base _attacker;
        private Skill_Base _skill;

        public void Initialize(Actor_Base attacker, Skill_Base skill)
        {
            _attacker = attacker;
            _skill = skill;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Actor_Base target = other.GetComponent<Actor_Base>();
                if (target != null)
                {
                    GlobalEventHub.SkillHub.Publish(new HitEvent(_attacker, target, _skill));
                }

                //Destroy(gameObject); 
            }
        }
    }
}