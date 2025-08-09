using OnewaveGames.Scripts.Actor.Component;
using OnewaveGames.Scripts.EventHub;
using OnewaveGames.Scripts.System.Library;
using OnewaveGames.Scripts.System.Table.TableData;
using UnityEngine;

public class Actor_Base : MonoBehaviour
{
    private EUnitType _unitType;
    private HealthComponent _healthComponent;

    public virtual void Initialize(EUnitType unitType)
    {
        _unitType = unitType;
            
        GlobalEventHub.SkillHub.Subscribe<HitEvent>(OnHit);
        _healthComponent = GetComponent<HealthComponent>();
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
            Skill_DataTable skillTable = (Skill_DataTable)SystemLibrary.GetTable(ETableType.Skill);
            if (!skillTable)
            {
                Debug.LogWarning("[Skill Table] is not exist!");
                return;
            }
            
            TakeDamage(skillTable.GetDamage(evt.SkillType));
        }
    }

    protected virtual void TakeDamage(int damage)
    {
        _healthComponent.TakeDamage(damage);
    }
}
