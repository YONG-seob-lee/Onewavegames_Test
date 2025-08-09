using OnewaveGames.Scripts.Projectile;
using OnewaveGames.Scripts.Skill.Indiacator;
using OnewaveGames.Scripts.System.Table.TableData;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OnewaveGames.Scripts.Effect
{
    [CreateAssetMenu(fileName = "ProjectileEffect", menuName = "Skills/Effects/Projectile Effect")]
    public class ProjectileEffect : SkillEffectSO
    {
        public GameObject projectilePrefab;
        public float projectileSpeed = 50f;
        public float indicatorRange = 20f;
        private float tableProjectileSpeed = 0f;
        private SkillIndicator _skillIndicator;

#if UNITY_EDITOR
        public bool testEditor = false;
#endif
        public override void Initialize(Skill_Entry skillEntry)
        {
            tableProjectileSpeed = skillEntry.ProjectileSpeed;
            indicatorRange = skillEntry.Range;
        }
        
        public override void Apply(GameObject caster, GameObject target)
        {
            if (caster == null || projectilePrefab == null)
            {
                Debug.LogError("Caster or Projectile Prefab is missing!");
                return;
            }

            Camera mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("메인 카메라를 찾을 수 없습니다.");
                return;
            }

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
                    
            RaycastHit hit;
            Vector3 targetDirection;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("Floor")))
            {
                targetDirection = (hit.point - caster.transform.position).normalized;
            }
            else
            {
                targetDirection = (ray.origin + ray.direction * indicatorRange - caster.transform.position).normalized;
            }

            targetDirection.y = 0f;
            
            // 마우스 방향으로 회전하면서 생성, 바닥에서 살짝 위쪽으로 떼서 발사.
            GameObject projectileObject = Instantiate(projectilePrefab, caster.transform.position + new Vector3(0, 1f, -2f), Quaternion.LookRotation(targetDirection));

            GrabProjectile projectileScript = projectileObject.GetComponent<GrabProjectile>();
            projectileScript.Initialize(caster, this);
            if (projectileScript != null)
            {
#if UNITY_EDITOR
                if (testEditor == true)
                {
                    projectileObject.GetComponent<Rigidbody>().velocity = targetDirection * projectileSpeed;
                }
                else
                {
                    projectileObject.GetComponent<Rigidbody>().velocity = targetDirection * tableProjectileSpeed;
                }
#else
                projectileObject.GetComponent<Rigidbody>().velocity = targetDirection * tableProjectileSpeed;
#endif
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
            
            if (skillIndicator != null)
            {
                Transform casterTransform = caster.transform;
                Vector2 mousePosition = Mouse.current.position.ReadValue();
                if (Camera.main != null)
                {
                    Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                    RaycastHit hit;
                    
                    Vector3 mouseHitPosition;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("Floor")))
                    {
                        mouseHitPosition = hit.point;
                    }
                    else
                    {
                        mouseHitPosition = ray.GetPoint(indicatorRange);
                    }

                    mouseHitPosition.y = 0f;
            
                    Vector3 casterPosition = casterTransform.position;
                    Vector3 direction = (mouseHitPosition - casterPosition).normalized;
                    Vector3 indicatorEndPosition = casterPosition + direction * indicatorRange;
                    Debug.Log(Vector3.Distance(casterPosition, indicatorEndPosition));
                    skillIndicator.Show(casterPosition, indicatorEndPosition);
                }
            }
        }

        public override void OnEnd(SkillIndicator skillIndicator, GameObject caster)
        {
            if (IsStart == false)
            {
                return;
            }
            
            if (skillIndicator != null)
            {
                skillIndicator.Hide();
            }
            IsStart = false;
            
            Apply(caster, null);
        }
    }
}