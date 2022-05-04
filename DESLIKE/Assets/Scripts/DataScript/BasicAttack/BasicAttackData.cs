using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackData : ScriptableObject
{
    public float start_Delay, end_Delay, range, atk_Dmg, charge_MP;
    public int atkCount;
    public SkillData extraSkillData;
    public AttackArea atkArea;

    public virtual void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {

    }

    protected void ChargeMP(HeroInfo heroInfo)
    {
        heroInfo.cur_Mp += charge_MP;
        if(heroInfo.cur_Mp > ((HeroData)heroInfo.castleData).mp)
        {
            heroInfo.cur_Mp = ((HeroData)heroInfo.castleData).mp;
        }
    }
}
