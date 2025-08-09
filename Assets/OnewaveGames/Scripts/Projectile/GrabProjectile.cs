using OnewaveGames.Scripts.Effect;
using UnityEngine;

namespace OnewaveGames.Scripts.Projectile
{
    public class GrabProjectile : MonoBehaviour
    {
        private GameObject _caster;
        private bool _hasHitTarget = false;
        private ProjectileEffect _projectileEffect;
        private Vector3 _initialPosition;
        
        // 초기화 함수: SkillData를 받아 투사체에 필요한 정보를 설정합니다.
        public void Initialize(GameObject caster, ProjectileEffect projectileEffect)
        {
            _caster = caster;
            _initialPosition = transform.position;
            _projectileEffect = projectileEffect;
        
            // 투사체 발사 속도를 SkillData에서 가져와 적용합니다.
            if (projectileEffect != null)
            {
                GetComponent<Rigidbody>().velocity = _caster.transform.forward * projectileEffect.projectileSpeed;
            }

            // 5초 후 자동으로 투사체 파괴
            Destroy(gameObject, 5f);
        }

        private void Update()
        {
            if (Vector3.Distance(_initialPosition, transform.position) >= _projectileEffect.indicatorRange)
            {
                Destroy(gameObject);
            }
        }
    
        // 충돌 감지
        private void OnCollisionEnter(Collision collision)
        {
            if (_hasHitTarget) return;

            if (collision.gameObject.CompareTag("Enemy") && collision.gameObject != _caster)
            {
                _hasHitTarget = true;
            
                _projectileEffect.Apply(_caster, collision.gameObject);
            }
        
            // 투사체 파괴
            Destroy(gameObject);
        }
    }
}