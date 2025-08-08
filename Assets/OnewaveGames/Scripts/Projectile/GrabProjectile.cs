using GAS;
using UnityEngine;

namespace OnewaveGames.Scripts.Projectile
{
    public class GrabProjectile : MonoBehaviour
    {
        private AbilitySystemComponent owner;
        private GameplayEffect grabEffect;

        public void Initialize(AbilitySystemComponent owner, GameplayEffect effect, float speed)
        {
            this.owner = owner;
            this.grabEffect = effect;

            // 투사체를 앞으로 날아가게 만듭니다.
            GetComponent<Rigidbody>().velocity = owner.transform.forward * speed;

            // 일정 시간 후 투사체 파괴
            Destroy(gameObject, 5f);
        }
    
        private void OnCollisionEnter(Collision collision)
        {
            // 충돌한 대상이 적(Enemy)인지 확인
            AbilitySystemComponent targetASC = collision.gameObject.GetComponent<AbilitySystemComponent>();
            if (targetASC != null && owner != null)
            {
                // 이 부분을 수정합니다. owner.ApplyGameplayEffect 함수를 사용합니다.
                // 첫 번째 인자는 스킬을 사용한 ASC(source), 두 번째는 명중된 ASC(target), 세 번째는 적용할 Effect입니다.
                owner.ApplyGameplayEffect(owner, targetASC, grabEffect);
            
                // 투사체 파괴
                Destroy(gameObject);
            }
        }
    }
}