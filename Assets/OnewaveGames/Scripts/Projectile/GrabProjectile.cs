using OnewaveGames.Scripts.Effect;
using OnewaveGames.Scripts.EventHub;
using OnewaveGames.Scripts.Skill;
using UnityEngine;

namespace OnewaveGames.Scripts.Projectile
{
    public class GrabProjectile : MonoBehaviour
    {
        private GameObject _caster;
        private bool _hasHitTarget = false;
        private ProjectileEffect _projectileEffect;
        private PullEffect _pullEffect;
        private Vector3 _initialPosition;
        
        public void Initialize(GameObject caster, ProjectileEffect projectileEffect)
        {
            _caster = caster;
            _initialPosition = transform.position;
            _projectileEffect = projectileEffect;
        
            if (projectileEffect != null)
            {
                GetComponent<Rigidbody>().velocity = _caster.transform.forward * projectileEffect.projectileSpeed;
            }

            // 버그 회피용 코드
            Destroy(gameObject, 5f);
        }

        private void Update()
        {
            if (Vector3.Distance(_initialPosition, transform.position) >= _projectileEffect.indicatorRange)
            {
                Destroy(gameObject);
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (_hasHitTarget) return;

            if (collision.gameObject.CompareTag("Enemy") && collision.gameObject != _caster)
            {
                _hasHitTarget = true;
                GlobalEventHub.SkillHub.Publish(new HitEvent(_caster, collision.gameObject, ESkillType.Grab));
                Destroy(gameObject);
            }
        
        }
    }
}