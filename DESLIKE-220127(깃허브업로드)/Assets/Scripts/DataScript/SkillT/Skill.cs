using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Skill : MonoBehaviour
{
    protected HeroInfo heroInfo;
    public SkillData skillData;

    protected virtual void Start()
    {
        heroInfo = GetComponent<HeroInfo>();
        if (heroInfo as SoldierInfo)//병사라면
        {
            SoldierBasic soldierBasic = GetComponent<SoldierBasic>();
            soldierBasic.skillDetect += Detect;
            soldierBasic.canSkill += CanSkillCheck;
            soldierBasic.skillHandler += UseSkill;
        }
    }

    public virtual bool CanSkillCheck()
    {
        return false;
    }

    public virtual void Detect()
    {

    }

    public virtual IEnumerator UseSkill(HeroInfo targetInfo)
    {
        yield return null;
    }
}