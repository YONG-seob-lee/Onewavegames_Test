using System.Collections.Generic;
using OnewaveGames.Scripts.TableData;

public struct SkillCastEvent
{
    public Actor_Base source;
    public Skill_Base skill;
}
public abstract class Skill_Base
{
    public SkillData skillData;
    
    private List<Effect> EffectList {get;} = new();

    public abstract bool ApplySkill(Actor_Base source, Actor_Base target);
    
    
}
