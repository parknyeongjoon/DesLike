using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Skill : MonoBehaviour
{
    protected HeroInfo heroInfo;
    protected SoldierBasic soldierBasic;

    public SkillData skillData;

    protected virtual void Awake()
    {
        heroInfo = GetComponent<HeroInfo>();
        soldierBasic = GetComponent<SoldierBasic>();
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