using System;
using OnewaveGames.Scripts.Skill;
using UnityEngine;

namespace OnewaveGames.Scripts.Effect
{
    [CreateAssetMenu(fileName = "ProjectileEffect", menuName = "Skills/Effects/Projectile Effect")]
    public class ProjectileEffect : SkillEffectSO
    {
        public GameObject projectilePrefab;
        public float projectileSpeed = 10f;
        
        public override void Apply(GameObject caster, GameObject target)
        {
            if (caster == null || projectilePrefab == null)
            {
                Debug.LogError("Caster or Projectile Prefab is missing!");
                return;
            }

            GameObject projectileObject = GameObject.Instantiate(projectilePrefab, caster.transform.position, caster.transform.rotation);
            projectileObject.GetComponent<Rigidbody>().velocity = caster.transform.forward * projectileSpeed;
        }
    }
}