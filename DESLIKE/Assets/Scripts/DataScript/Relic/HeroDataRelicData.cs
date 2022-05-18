using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDataRelicData : RelicData
{
    public virtual void Effect(HeroData heroData)
    {
        heroData.extraSkills += relicEffect;
    }

    public virtual void relicEffect(HeroInfo heroInfo)
    {

    }

    public virtual void extraEffect(HeroInfo heroInfo)
    {

    }

    public virtual bool ConditionCheck(HeroData heroData)
    {
        return false;
    }
}
