using System.Collections.Generic;
using OnewaveGames.Scripts.Manager;
using UnityEngine;

public class Actor_Base : MonoBehaviour
{
    public List<Skill_Base> skills = new List<Skill_Base>();

    private void OnEnable()
    {
        EventManager.Subscribe<SkillCastEvent>(HandleSkillCast);
    }

    private void OnDisable()
    {
        EventManager.Unsubcribe<SkillCastEvent>(HandleSkillCast);
    }

    public void ApplySkill(Actor_Base target)
    {
        
    }

    private void HandleSkillCast(SkillCastEvent data)
    {
        if (data.source == this)
        {
            
        }
    }
}
