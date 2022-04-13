using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackData : ScriptableObject
{
    public float start_Delay, end_Delay, range, atk_Dmg;
    public int atkCount;
    public SkillData extraSkillData;
    public AttackArea atkArea;

    public virtual void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {

    }
}
