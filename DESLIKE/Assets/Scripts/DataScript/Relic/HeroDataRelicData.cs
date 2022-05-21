using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDataRelicData : RelicData
{
    public virtual void Effect(HeroData heroData)//heroData.extraSkill에 넣어놓으면 heroInfo의 start에서 relicEffect가 자동으로 실행
    {
        heroData.extraSkills += relicEffect;
    }

    public virtual void RemoveEffect(HeroData heroData)
    {
        heroData.extraSkills -= relicEffect;
    }

    public virtual void relicEffect(HeroInfo heroInfo)//heroInfo의 어디에 추가할 건지 작성
    {

    }

    public virtual bool ConditionCheck(HeroData heroData)
    {
        return false;
    }
}
