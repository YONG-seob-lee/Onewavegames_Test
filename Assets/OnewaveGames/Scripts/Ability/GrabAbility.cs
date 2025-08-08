using GAS;
using OnewaveGames.Scripts.Projectile;
using UnityEngine;

namespace OnewaveGames.Scripts.Ability
{
    public class GrabAbility : GameplayAbility
    {
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public float projectileSpeed = 10f;
    [SerializeField] public GameplayEffect grabEffect;

    // GA가 Commit되었을 때 호출되는 함수입니다.
    // CommitAbility는 PreActivate, Activate, PostActivate를 모두 호출합니다.
    public override void CommitAbility(AbilitySystemComponent source, AbilitySystemComponent target, string activationGUID)
    {
        // base.CommitAbility(source, target, activationGUID); // 이 부분은 선택사항이지만 호출하는 것이 좋습니다.
        
        // 투사체 생성 및 발사 로직
        LaunchProjectile(source);
    }
    
    // GA의 활성화 조건 체크 (마나, 쿨타임 등)
    public override bool CanActivateAbility(AbilitySystemComponent src, AbilitySystemComponent target, string activationGUID, bool sendFailedEvent)
    {
        // 기존 GAS의 조건 체크 로직을 먼저 실행합니다.
        if (!base.CanActivateAbility(src, target, activationGUID, sendFailedEvent))
        {
            return false;
        }

        // TODO: 추가적인 조건 (예: 사거리, 시야)을 여기에 구현할 수 있습니다.
        // 예시: if (Vector3.Distance(src.transform.position, target.transform.position) > someRange) return false;
        
        return true;
    }

    private void LaunchProjectile(AbilitySystemComponent owner)
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab is not assigned.");
            return;
        }

        // 투사체를 플레이어 위치에서 생성하고, 앞으로 발사합니다.
        GameObject projectileObject = GameObject.Instantiate(projectilePrefab, owner.transform.position, owner.transform.rotation);
        
        // 투사체에 GameplayEffect를 전달하는 컴포넌트를 추가합니다.
        var projectileComponent = projectileObject.GetComponent<GrabProjectile>();
        if (projectileComponent == null)
        {
            projectileComponent = projectileObject.AddComponent<GrabProjectile>();
        }
        
        // 초기화 함수를 호출해서 투사체에 필요한 정보를 전달합니다.
        projectileComponent.Initialize(owner, grabEffect, projectileSpeed);
    }
    }
}