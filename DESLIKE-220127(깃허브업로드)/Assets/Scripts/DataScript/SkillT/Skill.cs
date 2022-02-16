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
            SoldierBehaviour soldierBehaviour = GetComponent<SoldierBehaviour>();
            soldierBehaviour.skillDetect += Detect;
            soldierBehaviour.canSkill += CanSkillCheck;
            soldierBehaviour.skillHandler += UseSkill;
        }
    }

    protected virtual bool CanSkillCheck()
    {
        return false;
    }

    protected virtual void Detect()
    {

    }

    protected virtual IEnumerator UseSkill(HeroInfo targetInfo)
    {
        yield return null;
    }

    public void SetHeroAction(int index)
    {
        HeroSkillUse heroSkillUse = GetComponent<HeroSkillUse>();
        heroSkillUse.skillHandlers[index] += UseSkill;
        heroSkillUse.canSkills[index] += CanSkillCheck;
        heroSkillUse.skillScripts[index] = this;
    }
}