using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Skill : MonoBehaviour
{
    [SerializeField]
    protected HeroInfo heroInfo;
    [SerializeField]
    protected SoldierBasic soldierBasic;
    public SkillData skillData;

    protected virtual void Start()
    {
        if (soldierBasic as SoldierBehaviour)
        {
            ((SoldierBehaviour)soldierBasic).skillDetect = Detect;
            ((SoldierBehaviour)soldierBasic).canSkill = CanSkillCheck;
            ((SoldierBehaviour)soldierBasic).skillHandler = UseSkill;
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
        ((HeroBehaviour)soldierBasic).skillHandler[index] = UseSkill;
        ((HeroBehaviour)soldierBasic).canSkill[index] = CanSkillCheck;
        ((HeroBehaviour)soldierBasic).skillDetect[index] = Detect;
    }
}