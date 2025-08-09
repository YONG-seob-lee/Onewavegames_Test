using OnewaveGames.Scripts.EventHub;
using UnityEngine;

public class Actor_Base : MonoBehaviour
{
    private EUnitType _unitType;

    public virtual void Initialize(EUnitType unitType)
    {
        _unitType = unitType;
            
        GlobalEventHub.SkillHub.Subscribe<HitEvent>(OnHit);
    }
    
    private void OnEnable()
    {
        if (GlobalEventHub.SkillHub != null)
        {
            GlobalEventHub.SkillHub.Subscribe<HitEvent>(HandleSkillCast);
        }
    }

    private void OnDisable()
    {
        if (GlobalEventHub.SkillHub != null)
        {
            GlobalEventHub.SkillHub.Unsubscribe<HitEvent>(HandleSkillCast);
        }
    }

    public void ApplySkill(Actor_Base target)
    {
        
    }

    private void HandleSkillCast(HitEvent data)
    {
    }
    
    protected virtual void OnHit(HitEvent evt)
    {
        if (evt.Target == gameObject)
        {
            TakeDamage(0);
        }
    }

    protected virtual void TakeDamage(int damage)
    {
        
    }
}
