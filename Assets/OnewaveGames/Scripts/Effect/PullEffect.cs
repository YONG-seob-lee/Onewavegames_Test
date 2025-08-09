using System.Collections;
using OnewaveGames.Scripts.EventHub;
using OnewaveGames.Scripts.Skill;
using OnewaveGames.Scripts.Skill.Indiacator;
using OnewaveGames.Scripts.System.Manager;
using OnewaveGames.Scripts.System.Table.TableData;
using UnityEngine;

namespace OnewaveGames.Scripts.Effect
{
    [CreateAssetMenu(fileName = "PullEffect", menuName = "Skills/Effects/Pull Effect")]
    public class PullEffect : SkillEffectSO
    {
        public float pullSpeed = 5f;
        public float pullDuration = 1f;

        private GameObject _activeTarget;
        private Coroutine _pullCoroutine;
        
        public override void Initialize(Skill_Entry skillEntry)
        {
            GlobalEventHub.SkillHub.Subscribe<HitEvent>(OnHit);
            pullSpeed = skillEntry.ProjectileSpeed;
        }

        private void OnHit(HitEvent hitEvent)
        {
            switch (hitEvent.SkillType)
            {
                case ESkillType.Grab:
                {
                    Apply(hitEvent.Attacker, hitEvent.Target);
                    break;
                }
                default:
                    break;
            }
        }

        public override void Apply(GameObject caster, GameObject target)
        {
            if (target == null || caster == null)
            {
                Debug.LogError("Target or Caster is missing!");
                return;
            }

            if (GameManager.Instance != null)
            {
                CapsuleCollider casterCollider = caster.GetComponentInChildren<CapsuleCollider>();
                if (casterCollider == null)
                {
                    Debug.LogError("CapsuleCollider is missing!");
                    return;
                }

                _activeTarget = target;
                _pullCoroutine = GameManager.Instance.StartCoroutine(PullCoroutine(caster.transform, target.transform.parent, casterCollider.radius));
            }
        }

        private IEnumerator PullCoroutine(Transform casterTransform, Transform targetTransform, float casterRadious)
        {
            float timer = 0f;
            while (timer < pullDuration)
            {
                // caster 와 target 사이 방향벡터
                Vector3 direction = (casterTransform.position - targetTransform.position).normalized;
                
                // caster의 캡슐 지름만큼 위치 계산
                Vector3 targetPosition = casterTransform.position - direction * casterRadious * 2;

                //targetPosition.x = 0;
                
                targetTransform.position = Vector3.MoveTowards(targetTransform.position, targetPosition,
                    pullSpeed * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }
        }

        public override void OnStart(SkillIndicator skillIndicator, GameObject caster, GameObject target)
        {
            IsStart = true;
        }

        public override void OnUpdate(SkillIndicator skillIndicator, GameObject caster, GameObject target)
        {
            
            if (IsStart == false)
            {
                return;
            }
        }

        public override void OnEnd(SkillIndicator skillIndicator, GameObject target)
        {
            IsStart = false;
        }
    }
}