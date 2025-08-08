using System.Collections;
using GAS;
using UnityEngine;

namespace OnewaveGames.Scripts.Effect
{
    public class GrabEffectController : MonoBehaviour
    {
        private AbilitySystemComponent asc;
        private Coroutine pullCoroutine;

        private void Awake()
        {
            asc = GetComponent<AbilitySystemComponent>();
            if (asc != null)
            {
                // GameplayEffect가 적용될 때 호출될 이벤트 구독
                asc.OnGameplayEffectApplied += OnEffectApplied;
                // GameplayEffect가 제거될 때 호출될 이벤트 구독
                asc.OnGameplayEffectRemoved += OnEffectRemoved;
            }
        }

        private void OnDestroy()
        {
            if (asc != null)
            {
                asc.OnGameplayEffectApplied -= OnEffectApplied;
                asc.OnGameplayEffectRemoved -= OnEffectRemoved;
            }
        }

        private void OnEffectApplied(GameplayEffect appliedGE)
        {
            // 적용된 효과가 GrabEffect인지 확인합니다.
            if (appliedGE is GrabEffect grabEffect)
            {
                // GrabEffect가 적용되면 끌어오기 코루틴 시작
                pullCoroutine = StartCoroutine(PullTarget(grabEffect.source.transform, transform, grabEffect.pullSpeed, grabEffect.durationValue));
            }
        }

        private void OnEffectRemoved(GameplayEffect removedGE)
        {
            // GrabEffect가 제거되면 끌어오기 코루틴 중지
            if (removedGE is GrabEffect && pullCoroutine != null)
            {
                StopCoroutine(pullCoroutine);
            }
        }

        private IEnumerator PullTarget(Transform source, Transform target, float pullSpeed, float duration)
        {
            float timer = 0f;
            while (timer < duration)
            {
                target.position = Vector3.MoveTowards(target.position, source.position, pullSpeed * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}